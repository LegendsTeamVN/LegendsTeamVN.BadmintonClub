using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Requests;
using LegendsTeamVN.BadmintonClub.Application.Features.Users.GetList;
using LegendsTeamVN.BadmintonClub.Application.Features.Users.Me;
using LegendsTeamVN.Core.Presentation.Abstractions;
using LegendsTeamVN.Core.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using LegendsTeamVN.Core.Identity.Authorization;

namespace LegendsTeamVN.BadmintonClub.Presentation.Endpoints;

public class UserEndpoint : EndpointGroupBase
{
    protected override string Name => "user";

    protected override void Map(RouteGroupBuilder group)
    {
        group.MapGet("me", GetMe)
             .WithName("GetMe")
             .WithSummary("Get current user details")
             .WithDescription("Returns details of the currently authenticated user.")
             .RequireAuthorization();

        group.MapGet("", GetUsers)
             .WithName("GetUsers")
             .WithSummary("Gets all users with pagination")
             .WithDescription("Retrieves a paged list of all users based on filter criteria.")
             .RequirePermission("System.Administrator");
    }

    private static async Task<IResult> GetMe(ISender sender)
    {
        var result = await sender.Send(new MeQuery());
        
        return result.Match(
            onSuccess: response => Results.Ok(response)
        );
    }

    private static async Task<IResult> GetUsers([AsParameters] GetUsersRequest request, ISender sender)
    {
        var result = await sender.Send(new GetUsersQuery(request));
        
        return result.Match(
            onSuccess: response => Results.Ok(response)
        );
    }
}
