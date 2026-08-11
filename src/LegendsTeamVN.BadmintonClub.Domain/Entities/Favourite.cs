using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Favourite : SoftDeletableEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid VenueId { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected Favourite() { }

    public Favourite(Guid userId, Guid venueId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        VenueId = venueId;
    }
}
