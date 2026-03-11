using Microsoft.AspNetCore.Mvc;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Customers.Models;
using ApexInvest.Modules.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApexInvest.Modules.Customers.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ApexDbContext _context;

    public CustomersController(ApexDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();
        return Ok(customers);
    }

    [HttpPost("enrollment")]
    public async Task<IActionResult> Enroll([FromBody] CreateCustomerRequest request)
    {
        try
        {
            var existing = await _context.Customers.AnyAsync(c => c.TaxId == request.TaxId);
            if (existing) return BadRequest(new { Error = "TaxId already registered.", Code = "CUSTOMER_TAXID_DUPLICATED" });

            var customer = new Customer(request.Name, request.TaxId, request.Email, request.MonthlyContribution);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Enroll), new { id = customer.Id }, customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message, Code = "INVALID_CONTRIBUTION" });
        }

    }

    [HttpPatch("{taxId}/deactivate")]
    public async Task<IActionResult> Deactivate(string taxId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.TaxId == taxId);
        if (customer == null) return NotFound(new { Message = "Customer not found." });

        customer.Deactivate();
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{taxId}/contribution")]
    public async Task<IActionResult> UpdateContribution(string taxId, [FromBody] decimal newAmount)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.TaxId == taxId);
            if (customer == null) return NotFound(new { Message = "Customer not found." });

            customer.UpdateContribution(newAmount);
            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}