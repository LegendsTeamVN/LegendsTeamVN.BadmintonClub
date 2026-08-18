namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Responses;

public record VenueResponse(
    Guid Id,
    string Name,
    string? Description,
    string Address,
    decimal Latitude,
    decimal Longitude,
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsActive
);