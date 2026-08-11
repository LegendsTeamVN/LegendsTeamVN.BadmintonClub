using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Review : SoftDeletableEntity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid VenueId { get; private set; }
    public int Rating { get; private set; }
    public string? Content { get; private set; }
    public string? ImageUrl { get; private set; }

    public virtual Venue Venue { get; private set; } = default!;

    protected Review() { }

    public Review(Guid userId, Guid venueId, int rating, string? content = null, string? imageUrl = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        VenueId = venueId;
        Rating = rating;
        Content = content;
        ImageUrl = imageUrl;
    }

    public void UpdateReview(int rating, string? content, string? imageUrl)
    {
        Rating = rating;
        Content = content;
        ImageUrl = imageUrl;
    }
}
