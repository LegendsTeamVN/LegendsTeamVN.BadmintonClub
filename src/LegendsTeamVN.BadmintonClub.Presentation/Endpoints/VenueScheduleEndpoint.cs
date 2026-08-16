using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Requests;
using LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Create;
using LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Delete;
using LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetById;
using LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetList;
using LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Update;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Presentation.Abstractions;
using LegendsTeamVN.Core.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LegendsTeamVN.BadmintonClub.Presentation.Endpoints;

public class VenueScheduleEndpoint : EndpointGroupBase
{
    protected override string Name => "venue-schedules";

    protected override void Map(RouteGroupBuilder group)
    {
        group.MapPost("", CreateVenueSchedule)
             .WithName("CreateVenueSchedule")
             .WithSummary("Creates a new venue schedule")
             .WithDescription("Creates a new operating schedule for a venue and returns the created ID.")
             .RequirePermission("VenueSchedules.Create");

        group.MapGet("", GetVenueSchedules)
             .WithName("GetVenueSchedules")
             .WithSummary("Gets venue schedules with pagination and filters")
             .WithDescription("Retrieves a paged list of venue schedules based on filter criteria.")
             .RequirePermission("VenueSchedules.Read");

        group.MapGet("{id:guid}", GetVenueScheduleById)
             .WithName("GetVenueScheduleById")
             .WithSummary("Gets a venue schedule by ID")
             .WithDescription("Retrieves the details of a specific venue schedule by its ID.")
             .RequirePermission("VenueSchedules.Read");

        group.MapPut("{id:guid}", UpdateVenueSchedule)
             .WithName("UpdateVenueSchedule")
             .WithSummary("Updates a venue schedule")
             .WithDescription("Updates the open/close time and status of an existing venue schedule.")
             .RequirePermission("VenueSchedules.Update");

        group.MapDelete("{id:guid}", DeleteVenueSchedule)
             .WithName("DeleteVenueSchedule")
             .WithSummary("Deletes a venue schedule")
             .WithDescription("Deletes an existing venue schedule by its ID.")
             .RequirePermission("VenueSchedules.Delete");
    }

    private static async Task<IResult> GetVenueSchedules([AsParameters] GetVenueSchedulesRequest request, ISender sender)
    {
        var result = await sender.Send(new GetVenueSchedulesQuery(request));
        return result.Match(
            onSuccess: responses => Results.Ok(responses)
        );
    }

    private static async Task<IResult> GetVenueScheduleById([FromRoute] Guid id, ISender sender)
    {
        var result = await sender.Send(new GetVenueScheduleByIdQuery(id));
        return result.Match(
            onSuccess: response => Results.Ok(response)
        );
    }

    private static async Task<IResult> CreateVenueSchedule([FromBody] CreateVenueScheduleRequest request, ISender sender)
    {
        var command = new CreateVenueScheduleCommand(request.VenueId, request.DayOfWeek, request.OpenTime, request.CloseTime, request.IsClosed);
        var result = await sender.Send(command);

        return result.Match(
            onSuccess: id => Results.CreatedAtRoute("CreateVenueSchedule", new { id }, id)
        );
    }

    private static async Task<IResult> UpdateVenueSchedule([FromRoute] Guid id, [FromBody] UpdateVenueScheduleRequest request, ISender sender)
    {
        var command = new UpdateVenueScheduleCommand(id, request.OpenTime, request.CloseTime, request.IsClosed);
        var result = await sender.Send(command);
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }

    private static async Task<IResult> DeleteVenueSchedule([FromRoute] Guid id, ISender sender)
    {
        var result = await sender.Send(new DeleteVenueScheduleCommand(id));
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }
}
