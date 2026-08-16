using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Requests;

public sealed record GetVenueSchedulesRequest : SearchFilter
{
    public Guid? VenueId { get; init; }
    public int? DayOfWeek { get; init; }
    public bool? IsClosed { get; init; }
}
