namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Responses;

public sealed record UserResponse(Guid Id, string Email, string? UserName, IList<string>? Roles, IList<string>? Permissions);
