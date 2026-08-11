using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class VenueSchedule : SoftDeletableEntity<Guid>
{
    public Guid VenueId { get; private set; }
    public int DayOfWeek { get; private set; }
    public TimeOnly OpenTime { get; private set; }
    public TimeOnly CloseTime { get; private set; }
    public bool IsClosed { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected VenueSchedule() { }

    public VenueSchedule(Guid venueId, int dayOfWeek, TimeOnly openTime, TimeOnly closeTime, bool isClosed = false)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsClosed = isClosed;
    }

    public void UpdateSchedule(TimeOnly openTime, TimeOnly closeTime, bool isClosed)
    {
        OpenTime = openTime;
        CloseTime = closeTime;
        IsClosed = isClosed;
    }
}
