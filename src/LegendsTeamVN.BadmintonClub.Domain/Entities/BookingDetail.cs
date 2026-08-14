using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class BookingDetail : SoftDeletableEntity<Guid>
{
    public Guid BookingId { get; private set; }
    public Guid CourtId { get; private set; }
    public DateTimeOffset TimeStart { get; private set; }
    public DateTimeOffset TimeEnd { get; private set; }
    public decimal Price { get; private set; }
    public BookingDetailStatus Status { get; private set; }

    public virtual Booking Booking { get; private set; } = default!;
    public virtual Court Court { get; private set; } = default!;
    public virtual Match? Match { get; private set; }

    protected BookingDetail() { }

    public BookingDetail(Guid bookingId, Guid courtId, DateTimeOffset timeStart, DateTimeOffset timeEnd, decimal price, BookingDetailStatus status = BookingDetailStatus.Pending)
    {
        Id = Guid.NewGuid();
        BookingId = bookingId;
        CourtId = courtId;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        Price = price;
        Status = status;
    }

    public void UpdateStatus(BookingDetailStatus status)
    {
        Status = status;
    }
}
