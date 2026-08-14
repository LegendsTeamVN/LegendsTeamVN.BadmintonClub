using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Match : AggregateRoot<Guid>
{
    public Guid BookingDetailId { get; private set; }
    public Guid HostId { get; private set; }
    public int MaxPlayers { get; private set; }
    public decimal PricePerPlayer { get; private set; }
    public MatchStatus Status { get; private set; }
    public string? Description { get; private set; }

    public virtual BookingDetail BookingDetail { get; private set; } = default!;
    public virtual ICollection<MatchPlayer> MatchPlayers { get; private set; } = new List<MatchPlayer>();

    protected Match() { }

    public Match(Guid bookingDetailId, Guid hostId, int maxPlayers, decimal pricePerPlayer, string? description = null)
    {
        Id = Guid.NewGuid();
        BookingDetailId = bookingDetailId;
        HostId = hostId;
        MaxPlayers = maxPlayers;
        PricePerPlayer = pricePerPlayer;
        Status = MatchStatus.Open;
        Description = description;
    }

    public void UpdateMatch(int maxPlayers, decimal pricePerPlayer, string? description)
    {
        MaxPlayers = maxPlayers;
        PricePerPlayer = pricePerPlayer;
        Description = description;
    }

    public void UpdateStatus(MatchStatus status)
    {
        Status = status;
    }
}
