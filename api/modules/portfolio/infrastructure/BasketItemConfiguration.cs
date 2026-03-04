using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApexInvest.Modules.Portfolio.Entities;

namespace ApexInvest.Modules.Portfolio.Infrastructure;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("baskets_items");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Ticker).IsRequired().HasMaxLength(10);
        builder.Property(i => i.Percentage).HasPrecision(5, 2).IsRequired();
    }
}