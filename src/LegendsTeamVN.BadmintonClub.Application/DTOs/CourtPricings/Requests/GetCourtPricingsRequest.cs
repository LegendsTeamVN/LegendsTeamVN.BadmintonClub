using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Requests;

public sealed record GetCourtPricingsRequest : SearchFilter
{
    public Guid? VenueId { get; init; }
    public int? DayOfWeek { get; init; }
    public bool? IsPeakHour { get; init; }
}
