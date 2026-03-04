namespace ApexInvest.Modules.Customers.Models;

public record CreateCustomerRequest(
    string Name,
    string TaxId,
    string Email,
    decimal MonthlyContribution
);