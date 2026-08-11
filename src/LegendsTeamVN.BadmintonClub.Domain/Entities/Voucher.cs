using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Voucher : AggregateRoot<Guid>
{
    public string Code { get; private set; } = default!;
    public int? DiscountPercent { get; private set; }
    public decimal? DiscountAmount { get; private set; }
    public decimal? MaxDiscount { get; private set; }
    public decimal? MinOrderValue { get; private set; }
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public int? UsageLimit { get; private set; }
    public bool IsActive { get; private set; }

    public virtual ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

    protected Voucher() { }

    public Voucher(
        string code,
        int? discountPercent = null,
        decimal? discountAmount = null,
        decimal? maxDiscount = null,
        decimal? minOrderValue = null,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        int? usageLimit = null,
        bool isActive = true)
    {
        Id = Guid.NewGuid();
        Code = code;
        DiscountPercent = discountPercent;
        DiscountAmount = discountAmount;
        MaxDiscount = maxDiscount;
        MinOrderValue = minOrderValue;
        StartDate = startDate;
        EndDate = endDate;
        UsageLimit = usageLimit;
        IsActive = isActive;
    }

    public void UpdateVoucher(
        int? discountPercent,
        decimal? discountAmount,
        decimal? maxDiscount,
        decimal? minOrderValue,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        int? usageLimit,
        bool isActive)
    {
        DiscountPercent = discountPercent;
        DiscountAmount = discountAmount;
        MaxDiscount = maxDiscount;
        MinOrderValue = minOrderValue;
        StartDate = startDate;
        EndDate = endDate;
        UsageLimit = usageLimit;
        IsActive = isActive;
    }

    public void SetActiveStatus(bool isActive)
    {
        IsActive = isActive;
    }
}
