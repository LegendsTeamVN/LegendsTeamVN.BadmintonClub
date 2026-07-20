using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Requests;

public sealed record GetCourtsRequest : SearchFilter
{
    public bool? IsAvailable { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
}
