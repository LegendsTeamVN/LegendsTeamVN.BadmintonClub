using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class VenueStaff : SoftDeletableEntity<Guid>
{
    public Guid VenueId { get; private set; }
    public Guid UserId { get; private set; }
    public StaffRole StaffRole { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected VenueStaff() { }

    public VenueStaff(Guid venueId, Guid userId, StaffRole staffRole)
    {
        Id = Guid.NewGuid();
        VenueId = venueId;
        UserId = userId;
        StaffRole = staffRole;
    }

    public void UpdateRole(StaffRole newRole)
    {
        StaffRole = newRole;
    }
}
