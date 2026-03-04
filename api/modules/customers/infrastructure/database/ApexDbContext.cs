using Microsoft.EntityFrameworkCore;
using ApexInvest.Modules.Customers;

namespace ApexInvest.Infrastructure.Database;

public class ApexDbContext : DbContext
{
    public ApexDbContext(DbContextOptions<ApexDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApexDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}