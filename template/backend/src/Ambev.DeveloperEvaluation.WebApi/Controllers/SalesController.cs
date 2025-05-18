using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Services;


namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly DefaultContext _context;
	private readonly DomainEventLogger _eventLogger;


    public SalesController(DefaultContext context, ILogger<SalesController> logger, SaleApplicationService saleService, DomainEventLogger eventLogger)
    {
        _context = context;
        //_eventLogger = eventLogger;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
    {
        if (request.Items.Count > 20)
            return BadRequest("Sales with more than 20 items are not allowed.");

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = Guid.NewGuid().ToString()[..8].ToUpper(), // Simples identificador
            Date = DateTime.UtcNow,
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            Items = request.Items.Select(i => new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
                //Discount = i.Discount
            }).ToList()
        };
		
		sale.ApplyAutomaticDisccount();

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        _eventLogger.LogSaleCreated(sale.Id);

        return CreatedAtAction(nameof(GetById), new { id = sale.Id }, new { sale.Id });
    }

    /// <summary>
    /// Returns a list of all sales.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Branch)
            .Include(s => s.Items).ThenInclude(i => i.Product)
            .ToListAsync();

        var response = sales.Select(s => new SaleResponse
        {
            Id = s.Id,
            SaleNumber = s.SaleNumber,
            Date = s.Date,
            CustomerName = s.Customer.Name,
            BranchName = s.Branch.Name,
            TotalAmount = s.TotalAmount,
            IsCancelled = s.IsCancelled,
            Items = s.Items.Select(i => new SaleItemResponse
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalItem = i.TotalItem
            }).ToList()
        });

        return Ok(response);
    }

    /// <summary>
    /// Gets a sale by its ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var s = await _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Branch)
            .Include(s => s.Items).ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (s == null)
            return NotFound();

        var response = new SaleResponse
        {
            Id = s.Id,
            SaleNumber = s.SaleNumber,
            Date = s.Date,
            CustomerName = s.Customer.Name,
            BranchName = s.Branch.Name,
            TotalAmount = s.TotalAmount,
            IsCancelled = s.IsCancelled,
            Items = s.Items.Select(i => new SaleItemResponse
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalItem = i.TotalItem
            }).ToList()
        };

        return Ok(response);
    }

    /// <summary>
    /// Cancels a sale.
    /// </summary>
    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale == null)
            return NotFound();

        if (sale.IsCancelled)
            return BadRequest("This sale is already cancelled.");

        sale.Cancel();

        await _context.SaveChangesAsync();

        _eventLogger.LogSaleCancelled(sale.Id);

        return NoContent();
    }

    /// <summary>
    /// Deletes a sale by ID.
    /// </summary>
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var sale = await _context.Sales.FindAsync(id);
		if (sale == null) return NotFound();

		if (sale.IsCancelled)
			return BadRequest("Não é possível excluir uma venda cancelada.");

		_context.Sales.Remove(sale);
		await _context.SaveChangesAsync();

		_eventLogger.LogSaleDeleted(sale.Id);
		return NoContent();
	}

}
