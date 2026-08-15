using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class CourtPricing : SoftDeletableEntity<Guid>
{
    public Guid VenueId { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public int? DayOfWeek { get; private set; }
    public decimal PricePerHour { get; private set; }
    public bool IsPeakHour { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected CourtPricing() { }

    public CourtPricing(Guid venueId, TimeOnly startTime, TimeOnly endTime, decimal pricePerHour, int? dayOfWeek = null, bool isPeakHour = false)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        StartTime = startTime;
        EndTime = endTime;
        PricePerHour = pricePerHour;
        DayOfWeek = dayOfWeek;
        IsPeakHour = isPeakHour;
    }

    public void UpdatePricing(TimeOnly startTime, TimeOnly endTime, decimal pricePerHour, int? dayOfWeek, bool isPeakHour)
    {
        StartTime = startTime;
        EndTime = endTime;
        PricePerHour = pricePerHour;
        DayOfWeek = dayOfWeek;
        IsPeakHour = isPeakHour;
    }
}
