namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Requests;

public sealed record CreateCourtRequest(
    string Name,
    string? Description,
    decimal PricePerHour
);
