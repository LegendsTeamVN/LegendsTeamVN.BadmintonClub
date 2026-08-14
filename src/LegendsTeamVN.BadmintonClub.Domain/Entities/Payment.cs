using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Payment : SoftDeletableEntity<Guid>
{
    public Guid BookingId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public string? TransactionCode { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }

    public virtual Booking Booking { get; private set; } = default!;

    protected Payment() { }

    public Payment(Guid bookingId, decimal amount, PaymentMethod paymentMethod, PaymentStatus paymentStatus = PaymentStatus.Pending, string? transactionCode = null)
    {
        Id = Guid.NewGuid();
        BookingId = bookingId;
        Amount = amount;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        TransactionCode = transactionCode;
    }

    public void MarkSuccess(string? transactionCode = null)
    {
        PaymentStatus = PaymentStatus.Success;
        TransactionCode = transactionCode ?? TransactionCode;
        PaidAt = DateTimeOffset.UtcNow;
    }

    public void MarkFailed()
    {
        PaymentStatus = PaymentStatus.Failed;
    }
}
