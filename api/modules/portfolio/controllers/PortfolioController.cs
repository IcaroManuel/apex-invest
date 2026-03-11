using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Portfolio.Entities;
using ApexInvest.Modules.Portfolio.Models;

namespace ApexInvest.Modules.Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly ApexDbContext _context;

    public PortfolioController(ApexDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetHistory()
    {
        var baskets = await _context.Set<Basket>()
            .Include(b => b.Items)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return Ok(baskets);
    }

    [HttpPost("baskets")]
    public async Task<IActionResult> CreateBasket([FromBody] CreateBasketRequest request)
    {
        try
        {
            var items = request.Items.Select(i => new BasketItem(i.Ticker, i.Percentage)).ToList();

            var newBasket = new Basket(request.Name, items);

            var activeBasket = await _context.Set<Basket>().FirstOrDefaultAsync(b => b.IsActive);
            if (activeBasket != null) activeBasket.Deactivate();

            _context.Set<Basket>().Add(newBasket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBasket), new { id = newBasket.Id }, newBasket);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message, Code = "INVALID_BASKET_RULES" });
        }
    }

    [HttpGet("baskets/active")]
    public async Task<IActionResult> GetActiveBasket()
    {
        var basket = await _context.Set<Basket>().Include(b => b.Items).FirstOrDefaultAsync(b => b.IsActive);
        if (basket == null) return NotFound(new { Message = "No active basket found." });
        return Ok(basket);
    }
}