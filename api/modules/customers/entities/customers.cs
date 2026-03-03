namespace ApexInvest.Modules.Customers;

public class Customer
{
    public long Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string TaxId { get; private set; } = string.Empty; //CPF
    public decimal MounthlyContribution { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime EnrollmentDate { get; private set; }

    private Customer() { }

    public Customer(string name, string taxId, string email, decimal mounthlyContribution)
    {
        if (mounthlyContribution < 100m) throw new ArgumentException("The minimum monthly contribution is R$ 100,00.");

        Name = name;
        TaxId = taxId;
        Email = email;
        MounthlyContribution = mounthlyContribution;
        IsActive = true;
        EnrollmentDate = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;
    public void UpdateContribution(decimal newAmount)
    {
        if (newAmount < 100m) throw new ArgumentException("The minimum monthly contribution is R$ 100,00.");

        MounthlyContribution = newAmount;
    }
}