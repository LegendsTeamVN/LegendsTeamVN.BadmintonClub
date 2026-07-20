using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Requests;
using LegendsTeamVN.BadmintonClub.Application.Features.Courts.Create;
using LegendsTeamVN.BadmintonClub.Application.Features.Courts.Delete;
using LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetById;
using LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetList;
using LegendsTeamVN.BadmintonClub.Application.Features.Courts.Update;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Presentation.Abstractions;
using LegendsTeamVN.Core.Presentation.Extensions;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LegendsTeamVN.BadmintonClub.Presentation.Endpoints;

public class CourtEndpoint : EndpointGroupBase
{
    protected override string Name => "courts";

    protected override void Map(RouteGroupBuilder group)
    {
        group.MapPost("", CreateCourt)
             .WithName("CreateCourt")
             .WithSummary("Creates a new badminton court")
             .WithDescription("Creates a new badminton court and returns the created ID.")
             .RequirePermission("Courts.Create");

        group.MapGet("", GetCourts)
             .WithName("GetCourts")
             .WithSummary("Gets all badminton courts with pagination")
             .WithDescription("Retrieves a paged list of all badminton courts based on filter criteria.")
             .RequirePermission("Courts.Read");

        group.MapGet("{id:guid}", GetCourtById)
             .WithName("GetCourtById")
             .WithSummary("Gets a badminton court by ID")
             .WithDescription("Retrieves the details of a specific badminton court by its ID.")
             .RequirePermission("Courts.Read");

        group.MapPut("{id:guid}", UpdateCourt)
             .WithName("UpdateCourt")
             .WithSummary("Updates a badminton court")
             .WithDescription("Updates the details of an existing badminton court.")
             .RequirePermission("Courts.Update");

        group.MapDelete("{id:guid}", DeleteCourt)
             .WithName("DeleteCourt")
             .WithSummary("Deletes a badminton court")
             .WithDescription("Deletes an existing badminton court by its ID.")
             .RequirePermission("Courts.Delete");
    }

    private static async Task<IResult> GetCourts([AsParameters] GetCourtsRequest request, ISender sender)
    {
        var result = await sender.Send(new GetCourtsQuery(request));
        return result.Match(
            onSuccess: responses => Results.Ok(responses)
        );
    }

    private static async Task<IResult> GetCourtById([FromRoute]Guid id, ISender sender)
    {
        var result = await sender.Send(new GetCourtByIdQuery(id));
        return result.Match(
            onSuccess: response => Results.Ok(response)
        );
    }

    private static async Task<IResult> UpdateCourt([FromRoute]Guid id, [FromBody]UpdateCourtRequest request, ISender sender)
    {
        var command = new UpdateCourtCommand(id, request.Name, request.Description, request.PricePerHour, request.IsAvailable);
        var result = await sender.Send(command);
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }

    private static async Task<IResult> DeleteCourt([FromRoute]Guid id, ISender sender)
    {
        var result = await sender.Send(new DeleteCourtCommand(id));
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }

    private static async Task<IResult> CreateCourt([FromBody]CreateCourtRequest request, ISender sender)
    {
        var command = new CreateCourtCommand(request.Name, request.Description, request.PricePerHour);
        var result = await sender.Send(command);

        return result.Match(
            onSuccess: id => Results.CreatedAtRoute("CreateCourt", new { id }, id)
        );
    }
}
