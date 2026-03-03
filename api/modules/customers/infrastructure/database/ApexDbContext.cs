using Microsoft.EntityFrameworkCore;

namespace ApexInvest.Infrastructure.Database;

public class ApexDbContext : DbContext
{
    public ApexDbContext(DbContextOptions<ApexDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApexDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}