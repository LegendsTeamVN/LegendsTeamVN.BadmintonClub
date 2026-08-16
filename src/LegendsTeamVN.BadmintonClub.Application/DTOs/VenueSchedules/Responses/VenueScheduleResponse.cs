namespace LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Responses;

public record VenueScheduleResponse(
    Guid Id,
    Guid VenueId,
    int DayOfWeek,
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsClosed,
    DateTimeOffset CreatedOnUtc,
    DateTimeOffset? ModifiedOnUtc
);
