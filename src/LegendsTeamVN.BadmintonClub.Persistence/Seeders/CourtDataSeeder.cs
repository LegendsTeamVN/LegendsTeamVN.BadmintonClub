using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.Core.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.BadmintonClub.Persistence.Seeders;

internal sealed class CourtDataSeeder(BadmintonDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.Set<Court>().AnyAsync(cancellationToken))
        {
            return;
        }

        var courts = new List<Court>
        {
            new Court("Sân số 1 (VIP)", "Sân thảm chất lượng cao", 150000),
            new Court("Sân số 2", "Sân thường", 100000),
            new Court("Sân số 3", "Sân thường", 100000)
        };

        await dbContext.Set<Court>().AddRangeAsync(courts, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
