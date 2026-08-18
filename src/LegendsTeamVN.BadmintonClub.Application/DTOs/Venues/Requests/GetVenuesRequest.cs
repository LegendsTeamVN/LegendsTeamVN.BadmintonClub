using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Requests;

public sealed record GetVenuesRequest : SearchFilter
{
    public string? Name { get; init; }
    public string? Address { get; init; }
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public TimeOnly? OpenTime { get; init; }
    public TimeOnly? CloseTime { get; init; }
}