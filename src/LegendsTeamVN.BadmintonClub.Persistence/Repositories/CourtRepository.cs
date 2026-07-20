using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Persistence.Repositories;

namespace LegendsTeamVN.BadmintonClub.Persistence.Repositories;

public class CourtRepository(BadmintonDbContext dbContext) : GenericRepository<BadmintonDbContext, Court, Guid>(dbContext), ICourtRepository
{
}
