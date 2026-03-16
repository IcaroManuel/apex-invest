namespace ApexInvest.Modules.Market.Entities;

public class StockPrice
{
    public long Id { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public DateTime ReferenceDate { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private StockPrice() { }

    public StockPrice(string ticker, decimal price, DateTime referenceDate)
    {
        Ticker = ticker;
        Price = price;
        ReferenceDate = referenceDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatedPrice(decimal newPrice, DateTime referenceDate)
    {
        Price = newPrice;
        ReferenceDate = referenceDate;
        UpdatedAt = DateTime.UtcNow;
    }
}