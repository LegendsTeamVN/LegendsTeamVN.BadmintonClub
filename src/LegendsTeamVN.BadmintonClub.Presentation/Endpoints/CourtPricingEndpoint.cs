using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Requests;
using LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Create;
using LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Delete;
using LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetById;
using LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetList;
using LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Update;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Presentation.Abstractions;
using LegendsTeamVN.Core.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LegendsTeamVN.BadmintonClub.Presentation.Endpoints;

public class CourtPricingEndpoint : EndpointGroupBase
{
    protected override string Name => "court-pricings";

    protected override void Map(RouteGroupBuilder group)
    {
        group.MapPost("", CreateCourtPricing)
             .WithName("CreateCourtPricing")
             .WithSummary("Creates a new court pricing rule")
             .WithDescription("Creates a new hourly price rule for a venue (e.g. peak hours, weekend pricing) and returns the created ID.")
             .RequirePermission("CourtPricings.Create");

        group.MapGet("", GetCourtPricings)
             .WithName("GetCourtPricings")
             .WithSummary("Gets court pricing rules with pagination and filters")
             .WithDescription("Retrieves a paged list of court pricing rules based on filter criteria.")
             .RequirePermission("CourtPricings.Read");

        group.MapGet("{id:guid}", GetCourtPricingById)
             .WithName("GetCourtPricingById")
             .WithSummary("Gets a court pricing rule by ID")
             .WithDescription("Retrieves the details of a specific court pricing rule by its ID.")
             .RequirePermission("CourtPricings.Read");

        group.MapPut("{id:guid}", UpdateCourtPricing)
             .WithName("UpdateCourtPricing")
             .WithSummary("Updates a court pricing rule")
             .WithDescription("Updates the price per hour, time window, and status of an existing court pricing rule.")
             .RequirePermission("CourtPricings.Update");

        group.MapDelete("{id:guid}", DeleteCourtPricing)
             .WithName("DeleteCourtPricing")
             .WithSummary("Deletes a court pricing rule")
             .WithDescription("Deletes an existing court pricing rule by its ID.")
             .RequirePermission("CourtPricings.Delete");
    }

    private static async Task<IResult> GetCourtPricings([AsParameters] GetCourtPricingsRequest request, ISender sender)
    {
        var result = await sender.Send(new GetCourtPricingsQuery(request));
        return result.Match(
            onSuccess: responses => Results.Ok(responses)
        );
    }

    private static async Task<IResult> GetCourtPricingById([FromRoute] Guid id, ISender sender)
    {
        var result = await sender.Send(new GetCourtPricingByIdQuery(id));
        return result.Match(
            onSuccess: response => Results.Ok(response)
        );
    }

    private static async Task<IResult> CreateCourtPricing([FromBody] CreateCourtPricingRequest request, ISender sender)
    {
        var command = new CreateCourtPricingCommand(
            request.VenueId,
            request.StartTime,
            request.EndTime,
            request.PricePerHour,
            request.DayOfWeek,
            request.IsPeakHour
        );
        var result = await sender.Send(command);

        return result.Match(
            onSuccess: id => Results.CreatedAtRoute("CreateCourtPricing", new { id }, id)
        );
    }

    private static async Task<IResult> UpdateCourtPricing([FromRoute] Guid id, [FromBody] UpdateCourtPricingRequest request, ISender sender)
    {
        var command = new UpdateCourtPricingCommand(
            id,
            request.StartTime,
            request.EndTime,
            request.PricePerHour,
            request.DayOfWeek,
            request.IsPeakHour
        );
        var result = await sender.Send(command);
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }

    private static async Task<IResult> DeleteCourtPricing([FromRoute] Guid id, ISender sender)
    {
        var result = await sender.Send(new DeleteCourtPricingCommand(id));
        return result.Match(
            onSuccess: () => Results.NoContent()
        );
    }
}
