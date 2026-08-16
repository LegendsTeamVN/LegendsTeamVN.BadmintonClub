namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Requests;

public sealed record CreateVenueRequest(
    string Name,
    string? Description,
    string Address,
    decimal Latitude,
    decimal Longitude,
    TimeOnly OpenTime,
    TimeOnly CloseTime
);