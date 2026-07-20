namespace LegendsTeamVN.Core.Identity.Abstractions;

public interface ICurrentUserService
{
    string? UserName { get; }
    string? Email { get; }
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
    List<string>? Roles { get; }
    List<string>? Permissions { get; }
}
