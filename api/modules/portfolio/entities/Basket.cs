using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;

namespace ApexInvest.Modules.Portfolio.Entities;

public class Basket
{
    public long Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime DeactivatedAt { get; private set; }

    private readonly List<BasketItem> _items = new();
    public virtual IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    private Basket() { }

    public Basket(string name, List<BasketItem> items)
    {
        if (items == null || items.Count != 5) throw new ArgumentException("The basket must contain exactly 5 stocks.");

        var totalPercentage = items.Sum(x => x.Percentage);
        if (totalPercentage != 100m) throw new ArgumentException($"The sum of percentages must be 100%. Current sum: {totalPercentage}%");
        if (items.Any(x => x.Percentage <= 0)) throw new ArgumentException("Each stock percentage must be greater than 0%.");

        Name = name;
        _items = items;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

    }

    public void Deactivate()
    {
        IsActive = false;
        DeactivatedAt = DateTime.UtcNow;
    }

}