using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Requests;
using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.GetList;

public sealed record GetUsersQuery(GetUsersRequest Filter) : IQuery<PagedResult<UserResponse>>;
