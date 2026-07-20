using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Requests;

public sealed record GetUsersRequest : SearchFilter
{
    public string? Email { get; init; }

    public string? UserName { get; init; }
}
