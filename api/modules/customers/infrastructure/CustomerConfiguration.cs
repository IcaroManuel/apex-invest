using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApexInvest.Modules.Customers;

namespace ApexInvest.Modules.Customers.Infrastructure;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.TaxId).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.TaxId).IsRequired().HasMaxLength(11);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
        builder.Property(c => c.MonthlyContribution).HasPrecision(18, 2);
        builder.Property(c => c.IsActive).IsRequired();
        builder.Property(c => c.EnrollmentDate).IsRequired();

    }
}