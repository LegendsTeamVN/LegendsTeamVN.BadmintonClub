using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.Me;

public record MeQuery() : IQuery<MeResponse>;

public record MeResponse(Guid UserId, string Email);
