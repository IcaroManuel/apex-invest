namespace ApexInvest.Modules.Portfolio.Entities;

public class BasketItem
{
    public long Id { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public decimal Percentage { get; private set; }
    public long BasketId { get; private set; }

    private BasketItem() { }

    public BasketItem(string ticker, decimal percentage)
    {
        Ticker = ticker.ToUpper().Trim();
        Percentage = percentage;
    }
}