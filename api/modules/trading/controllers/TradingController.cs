using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Trading.Services;
using ApexInvest.Modules.Trading.Models;

namespace ApexInvest.Modules.Trading.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradingController : ControllerBase
{
    private readonly PurchaseEngineService _purchaseEngine;
    private readonly ApexDbContext _context;

    public TradingController(PurchaseEngineService purchaseEngine, ApexDbContext context)
    {
        _purchaseEngine = purchaseEngine;
        _context = context;
    }

    [HttpPost("process-monthly-orders")]
    public async Task<IActionResult> ProcessOrders()
    {
        try
        {
            await _purchaseEngine.ProcessMonthlyAports();
            return Ok(new { Message = "Monthly purchase orders processed successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.PurchaseOrders.OrderByDescending(o => o.CreatedAt).ToListAsync();
        return Ok(orders);
    }

    [HttpGet("my-position/{taxId}")]
    public async Task<IActionResult> GetPosition(string taxId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.TaxId == taxId);
        if (customer == null) return NotFound("Cliente não encontrado.");

        var orders = await _context.PurchaseOrders
            .Where(o => o.CustomerId == customer.Id)
            .ToListAsync();

        var position = orders.GroupBy(o => o.Ticker)
            .Select(group =>
            {
                var ticker = group.Key;
                var totalInvested = group.Sum(o => o.AllocatedAmount);

                var currentPrice = _context.StockPrices
                    .Where(p => p.Ticker == ticker)
                    .OrderByDescending(p => p.ReferenceDate)
                    .Select(p => p.Price)
                    .FirstOrDefault();

                var totalQuantity = (int)Math.Floor(totalInvested / (currentPrice > 0 ? currentPrice : 1));

                return new PortfolioPositionResponse(
                    Ticker: ticker,
                    TotalQuantity: totalQuantity,
                    AveragePrice: totalQuantity > 0 ? totalInvested / totalQuantity : 0,
                    TotalInvested: totalInvested,
                    CurrentValue: totalQuantity * currentPrice,
                    ProfitLoss: (totalQuantity * currentPrice) - totalInvested
                );
            }).ToList();

        return Ok(new
        {
            CustomerName = customer.Name,
            TotalPortfolioValue = position.Sum(p => p.CurrentValue),
            Assets = position
        });
    }
}