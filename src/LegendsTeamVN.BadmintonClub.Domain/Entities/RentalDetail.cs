using LegendsTeamVN.BadmintonClub.Domain.Enums;
using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class RentalDetail : SoftDeletableEntity<Guid>
{
    public Guid BookingId { get; private set; }
    public Guid RentalItemId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public RentalDetailStatus Status { get; private set; }

    public virtual Booking Booking { get; private set; } = default!;
    public virtual RentalItem RentalItem { get; private set; } = default!;

    protected RentalDetail() { }

    public RentalDetail(Guid bookingId, Guid rentalItemId, int quantity, decimal price, RentalDetailStatus status = RentalDetailStatus.Pending)
    {
        Id = Guid.NewGuid();
        BookingId = bookingId;
        RentalItemId = rentalItemId;
        Quantity = quantity;
        Price = price;
        Status = status;
    }

    public void UpdateStatus(RentalDetailStatus status)
    {
        Status = status;
    }
}
