namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Responses;

public record CourtResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal PricePerHour,
    bool IsAvailable
);
