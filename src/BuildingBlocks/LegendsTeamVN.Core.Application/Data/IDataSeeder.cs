namespace LegendsTeamVN.Core.Application.Data;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
