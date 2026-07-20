using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.Core.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.BadmintonClub.Persistence;

public class BadmintonDbContext(DbContextOptions<BadmintonDbContext> options, IPublisher publisher) : DbContextUnitOfWork<BadmintonDbContext>(options, publisher)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadmintonDbContext).Assembly);
    }
}
