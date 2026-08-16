using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Persistence.Repositories;

namespace LegendsTeamVN.BadmintonClub.Persistence.Repositories;

public class VenueScheduleRepository(BadmintonDbContext dbContext) : GenericRepository<BadmintonDbContext, VenueSchedule, Guid>(dbContext), IVenueScheduleRepository
{
}
