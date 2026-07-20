using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Court : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal PricePerHour { get; private set; }
    public bool IsAvailable { get; private set; }

    protected Court() { }

    public Court(string name, string? description, decimal pricePerHour)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        IsAvailable = true;
    }

    public void UpdateDetails(string name, string? description, decimal pricePerHour)
    {
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}
