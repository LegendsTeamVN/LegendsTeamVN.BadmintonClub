namespace LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Requests;

public record CreateCourtPricingRequest(
    Guid VenueId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    decimal PricePerHour,
    int? DayOfWeek = null,
    bool IsPeakHour = false
);
