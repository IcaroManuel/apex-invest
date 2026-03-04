namespace ApexInvest.Modules.Portfolio.Models;

public record BasketItemRequest(
    string Ticker,
    decimal Percentage
);