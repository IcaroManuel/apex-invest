namespace ApexInvest.Modules.Trading.Models;

public record PortfolioPositionResponse(
    string Ticker,
    int TotalQuantity,
    decimal AveragePrice,
    decimal TotalInvested,
    decimal CurrentValue,
    decimal ProfitLoss
);