namespace ApexInvest.Modules.Portfolio.Models;

public record CreateBasketRequest(
    string Name,
    List<BasketItemRequest> Items
);