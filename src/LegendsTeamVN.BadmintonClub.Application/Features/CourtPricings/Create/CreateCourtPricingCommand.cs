using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Create;

public sealed record CreateCourtPricingCommand(
    Guid VenueId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    decimal PricePerHour,
    int? DayOfWeek,
    bool IsPeakHour
) : ICommand<Guid>;
