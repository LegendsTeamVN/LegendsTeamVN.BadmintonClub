using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Logout;

public record LogoutCommand(string Email) : ICommand;
