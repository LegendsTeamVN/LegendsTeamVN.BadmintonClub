using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class RentalItem : AggregateRoot<Guid>
{
    public Guid VenueId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;
    public virtual ICollection<RentalDetail> RentalDetails { get; private set; } = new List<RentalDetail>();

    protected RentalItem() { }

    public RentalItem(Guid venueId, string name, int quantity, decimal price)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public void UpdateInfo(string name, int quantity, decimal price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}
