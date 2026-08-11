using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class VenueImage : SoftDeletableEntity<Guid>
{
    public Guid VenueId { get; private set; }
    public string ImageUrl { get; private set; } = default!;
    public bool IsPrimary { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected VenueImage() { }

    public VenueImage(Guid venueId, string imageUrl, bool isPrimary = false)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        ImageUrl = imageUrl;
        IsPrimary = isPrimary;
    }

    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
    }
}
