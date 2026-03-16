using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApexInvest.Modules.Market.Entities;

namespace ApexInvest.Modules.Market.Infrastructure;

public class StockPriceConfiguration : IEntityTypeConfiguration<StockPrice>
{
    public void Configure(EntityTypeBuilder<StockPrice> builder)
    {
        builder.ToTable("stock_prices");
        builder.HasKey(sp => sp.Id);
        builder.Property(sp => sp.Ticker).IsRequired().HasMaxLength(10);
        builder.Property(sp => sp.Price).HasPrecision(18, 2).IsRequired();
        builder.Property(sp => sp.ReferenceDate).IsRequired();
        builder.Property(sp => sp.UpdatedAt).IsRequired();

        builder.HasIndex(s => s.Ticker);
    }
}