using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Booking : AggregateRoot<Guid>
{
    public Guid UserId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal FinalPrice { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTimeOffset BookingTime { get; private set; }

    public virtual Voucher? Voucher { get; private set; }
    public virtual ICollection<BookingDetail> BookingDetails { get; private set; } = new List<BookingDetail>();
    public virtual ICollection<RentalDetail> RentalDetails { get; private set; } = new List<RentalDetail>();
    public virtual ICollection<Payment> Payments { get; private set; } = new List<Payment>();
    public virtual BookingCancellation? Cancellation { get; private set; }

    protected Booking() { }

    public Booking(Guid userId, decimal totalPrice, Guid? voucherId = null, decimal discountAmount = 0, decimal finalPrice = 0)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        VoucherId = voucherId;
        TotalPrice = totalPrice;
        DiscountAmount = discountAmount;
        FinalPrice = finalPrice > 0 ? finalPrice : (totalPrice - discountAmount);
        Status = BookingStatus.Pending;
        BookingTime = DateTimeOffset.UtcNow;
    }

    public void UpdateStatus(BookingStatus status)
    {
        Status = status;
    }

    public void ApplyVoucher(Guid voucherId, decimal discountAmount)
    {
        VoucherId = voucherId;
        DiscountAmount = discountAmount;
        FinalPrice = Math.Max(0, TotalPrice - DiscountAmount);
    }
}
