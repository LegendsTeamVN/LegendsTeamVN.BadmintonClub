namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Requests;

public sealed record UpdateCourtRequest(
    string Name,
    string? Description,
    decimal PricePerHour,
    bool IsAvailable
);
