namespace ApexInvest.Modules.Market.Services;

public class B3ParserService
{
    public decimal ParsePrice(string line)
    {
        string priceRaw = line.Substring(108, 13).Trim();
        return decimal.Parse(priceRaw) / 100m;
    }

    public string ParseTicker(string line)
    {
        return line.Substring(12, 12).Trim();
    }
}