using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Court : AggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal PricePerHour { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsActive { get; private set; }

    public Guid VenueId { get; private set; }
    public Guid SportTypeId { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;
    public virtual SportType SportType { get; private set; } = default!;

    public virtual ICollection<CourtPricing> Pricings { get; private set; } = new List<CourtPricing>();
    public virtual ICollection<CourtMaintenance> Maintenances { get; private set; } = new List<CourtMaintenance>();
    public virtual ICollection<BookingDetail> BookingDetails { get; private set; } = new List<BookingDetail>();

    protected Court() { }

    public Court(string name, string? description, decimal pricePerHour, Guid venueId = default, Guid sportTypeId = default)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        IsAvailable = true;
        IsActive = true;
        VenueId = venueId;
        SportTypeId = sportTypeId;
    }

    public void UpdateDetails(string name, string? description, decimal pricePerHour, Guid venueId = default, Guid sportTypeId = default)
    {
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        if (venueId != default) VenueId = venueId;
        if (sportTypeId != default) SportTypeId = sportTypeId;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        IsActive = isAvailable;
    }

    public void SetActiveStatus(bool isActive)
    {
        IsActive = isActive;
        IsAvailable = isActive;
    }
}
