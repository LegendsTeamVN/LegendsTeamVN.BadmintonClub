using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Update;

public sealed record UpdateCourtPricingCommand(
    Guid Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    decimal PricePerHour,
    int? DayOfWeek,
    bool IsPeakHour
) : ICommand;
