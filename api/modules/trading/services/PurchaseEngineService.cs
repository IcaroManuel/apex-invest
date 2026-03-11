using Microsoft.EntityFrameworkCore;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Portfolio.Entities;
using ApexInvest.Modules.Trading.Entities;

namespace ApexInvest.Modules.Trading.Services;

public class PurchaseEngineService
{
    private readonly ApexDbContext _context;
    public PurchaseEngineService(ApexDbContext context)
    {
        _context = context;
    }

    public async Task ProcessMonthlyAports()
    {
        var activeBasket = await _context.Set<Basket>().Include(b => b.Items).FirstOrDefaultAsync(b => b.IsActive);
        if (activeBasket == null) throw new Exception("No active basket found to process orders.");

        var activeCustomers = await _context.Customers.Where(c => c.IsActive).ToListAsync();
        foreach (var customer in activeCustomers)
        {
            foreach (var item in activeBasket.Items)
            {
                decimal amountToInvest = customer.MonthlyContribution * (item.Percentage / 100m);
                var order = new PurchaseOrder(customer.Id, item.Ticker, amountToInvest, DateTime.UtcNow);
                _context.Set<PurchaseOrder>().Add(order);
            }
        }

        await _context.SaveChangesAsync();
    }
}