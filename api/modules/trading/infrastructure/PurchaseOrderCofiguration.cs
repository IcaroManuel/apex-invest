using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApexInvest.Modules.Trading.Entities;

namespace ApexInvest.Modules.Trading.Infrastructure;

public class PurchaseOrderCofiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("purchase_order");
        builder.HasKey(po => po.Id);
        builder.Property(po => po.Ticker).IsRequired().HasMaxLength(10);
        builder.Property(po => po.AllocatedAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(po => po.CustomerId).IsRequired();
        builder.Property(po => po.CreatedAt).IsRequired();
    }
}