using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Persistence.Repositories;

namespace LegendsTeamVN.BadmintonClub.Persistence.Repositories;

public class CourtPricingRepository(BadmintonDbContext dbContext) : GenericRepository<BadmintonDbContext, CourtPricing, Guid>(dbContext), ICourtPricingRepository
{
}
