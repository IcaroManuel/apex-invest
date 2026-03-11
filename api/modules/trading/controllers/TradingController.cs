using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Trading.Services;

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
}