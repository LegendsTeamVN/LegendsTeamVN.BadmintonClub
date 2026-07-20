using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.Me;

public record MeQuery() : IQuery<UserResponse>;
