using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class VenueSportType : Entity<Guid>
{
    public Guid VenueId { get; private set; }
    public Guid SportTypeId { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;
    public virtual SportType SportType { get; private set; } = default!;

    protected VenueSportType() { }

    public VenueSportType(Guid venueId, Guid sportTypeId)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        SportTypeId = sportTypeId;
    }
}
