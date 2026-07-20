using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Entities;

using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetById;

public sealed class GetCourtByIdQueryHandler(ICourtRepository courtRepository) : IQueryHandler<GetCourtByIdQuery, CourtResponse>
{
    public async Task<Result<CourtResponse>> Handle(GetCourtByIdQuery request, CancellationToken cancellationToken)
    {
        var court = await courtRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (court is null)
        {
            return Result.Failure<CourtResponse>(Error.NotFound("Court.NotFound", "The court with the specified ID was not found."));
        }

        var response = new CourtResponse(court.Id, court.Name, court.Description, court.PricePerHour, court.IsAvailable);
        
        return Result.Success(response);
    }
}
