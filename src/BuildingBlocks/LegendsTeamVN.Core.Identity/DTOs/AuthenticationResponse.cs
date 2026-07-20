namespace LegendsTeamVN.Core.Identity.DTOs;

public record AuthenticationResponse
{
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? RefreshTokenExpiryTime { get; init; }
}
