namespace LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Requests;

public record UpdateCourtPricingRequest(
    TimeOnly StartTime,
    TimeOnly EndTime,
    decimal PricePerHour,
    int? DayOfWeek,
    bool IsPeakHour
);
