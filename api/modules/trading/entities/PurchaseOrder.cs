namespace ApexInvest.Modules.Trading.Entities;

public class PurchaseOrder
{
    public long Id { get; private set; }
    public long CustomerId { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public decimal AllocatedAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PurchaseOrder() { }
    public PurchaseOrder(long customerId, string ticker, decimal allocatedAmount, DateTime createdAt)
    {
        CustomerId = customerId;
        Ticker = ticker;
        AllocatedAmount = allocatedAmount;
        CreatedAt = createdAt;
    }
}