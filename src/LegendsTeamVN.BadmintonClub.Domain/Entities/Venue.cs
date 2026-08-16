using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Venue : AggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }
    public bool IsActive { get; private set; }
    public string? Description { get; private set; }
    public TimeOnly OpenTime { get; private set; }
    public TimeOnly CloseTime { get; private set; }

    public virtual ICollection<VenueSchedule> Schedules { get; private set; } = new List<VenueSchedule>();
    public virtual ICollection<VenueImage> Images { get; private set; } = new List<VenueImage>();
    public virtual ICollection<VenueSportType> VenueSportTypes { get; private set; } = new List<VenueSportType>();
    public virtual ICollection<Court> Courts { get; private set; } = new List<Court>();
    public virtual ICollection<RentalItem> RentalItems { get; private set; } = new List<RentalItem>();
    public virtual ICollection<VenueStaff> Staffs { get; private set; } = new List<VenueStaff>();
    public virtual ICollection<Review> Reviews { get; private set; } = new List<Review>();
    public virtual ICollection<Favourite> Favourites { get; private set; } = new List<Favourite>();
    public virtual ICollection<CourtPricing> Pricings { get; private set; } = new List<CourtPricing>();

    protected Venue() { }

    public Venue(string name, string address, decimal latitude, decimal longitude, TimeOnly openTime, TimeOnly closeTime, string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        OpenTime = openTime;
        CloseTime = closeTime;
        Description = description;
        IsActive = true;
    }

    public void UpdateInfo(string name, string address, decimal latitude, decimal longitude, TimeOnly openTime, TimeOnly closeTime, string? description)
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        OpenTime = openTime;
        CloseTime = closeTime;
        Description = description;
    }

    public void SetActiveStatus(bool isActive)
    {
        IsActive = isActive;
    }
}
