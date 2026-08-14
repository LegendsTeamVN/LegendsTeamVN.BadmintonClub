using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class CourtMaintenance : SoftDeletableEntity<Guid>
{
    public Guid CourtId { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }
    public string? Reason { get; private set; }

    public virtual Court Court { get; private set; } = default!;

    protected CourtMaintenance() { }

    public CourtMaintenance(Guid courtId, DateTimeOffset startTime, DateTimeOffset endTime, string? reason = null)
    {
        Id = Guid.NewGuid();
        CourtId = courtId;
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason;
    }

    public void UpdateMaintenance(DateTimeOffset startTime, DateTimeOffset endTime, string? reason)
    {
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason;
    }
}
