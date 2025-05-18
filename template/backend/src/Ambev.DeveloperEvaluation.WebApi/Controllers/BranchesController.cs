using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly DefaultContext _context;

    public BranchesController(DefaultContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Branch branch)
    {
        branch.Id = Guid.NewGuid();
        _context.Branches.Add(branch);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _context.Branches.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var branch = await _context.Branches.FindAsync(id);
        return branch is null ? NotFound() : Ok(branch);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Branch input)
    {
        var branch = await _context.Branches.FindAsync(id);
        if (branch is null) return NotFound();

        branch.Name = input.Name;
        branch.Region = input.Region;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var branch = await _context.Branches.FindAsync(id);
        if (branch is null) return NotFound();

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
