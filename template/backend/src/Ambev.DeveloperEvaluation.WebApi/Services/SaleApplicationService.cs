using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Services;

/// <summary>
/// Application layer to handle sale creation logic.
/// </summary>
public class SaleApplicationService
{
    private readonly DefaultContext _context;

    public SaleApplicationService(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateSaleAsync(CreateSaleRequest request)
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = Guid.NewGuid().ToString()[..8].ToUpper(),
            Date = DateTime.UtcNow,
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            Items = request.Items.Select(i => new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        sale.ApplyAutomaticDisccount();

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return sale.Id;
    }
}
