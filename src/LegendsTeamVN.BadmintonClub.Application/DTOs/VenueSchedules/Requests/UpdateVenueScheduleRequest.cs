namespace LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Requests;

public record UpdateVenueScheduleRequest(
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsClosed
);
