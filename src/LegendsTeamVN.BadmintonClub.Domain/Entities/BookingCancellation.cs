using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class BookingCancellation : SoftDeletableEntity<Guid>
{
    public Guid BookingId { get; private set; }
    public string? Reason { get; private set; }
    public decimal RefundAmount { get; private set; }
    public RefundStatus RefundStatus { get; private set; }
    public DateTimeOffset RequestedAt { get; private set; }
    public DateTimeOffset? ProcessedAt { get; private set; }

    public virtual Booking Booking { get; private set; } = default!;

    protected BookingCancellation() { }

    public BookingCancellation(Guid bookingId, string? reason, decimal refundAmount, RefundStatus refundStatus = RefundStatus.Pending)
    {
        Id = Guid.NewGuid();
        BookingId = bookingId;
        Reason = reason;
        RefundAmount = refundAmount;
        RefundStatus = refundStatus;
        RequestedAt = DateTimeOffset.UtcNow;
    }

    public void ProcessRefund(RefundStatus status)
    {
        RefundStatus = status;
        ProcessedAt = DateTimeOffset.UtcNow;
    }
}
