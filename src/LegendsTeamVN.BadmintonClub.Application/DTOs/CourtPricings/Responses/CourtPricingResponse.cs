namespace LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Responses;

public record CourtPricingResponse(
    Guid Id,
    Guid VenueId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    decimal PricePerHour,
    int? DayOfWeek,
    bool IsPeakHour,
    DateTimeOffset CreatedOnUtc,
    DateTimeOffset? ModifiedOnUtc
);
