using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApexInvest.Modules.Portfolio.Entities;

namespace ApexInvest.Modules.Portfolio.Infrastructure;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("baskets");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
        builder.Property(b => b.IsActive).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.HasMany(b => b.Items).WithOne().HasForeignKey(i => i.BasketId).OnDelete(DeleteBehavior.Cascade);
    }
}