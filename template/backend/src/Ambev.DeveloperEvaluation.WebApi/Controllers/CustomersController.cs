using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly DefaultContext _context;

    public CustomersController(DefaultContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _context.Customers.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Customer input)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) return NotFound();

        customer.Name = input.Name;
        customer.DocumentNumber = input.DocumentNumber;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
