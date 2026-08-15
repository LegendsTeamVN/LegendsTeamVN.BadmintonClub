namespace LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Requests;

public record CreateVenueScheduleRequest(
    Guid VenueId,
    int DayOfWeek,
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsClosed = false
);
