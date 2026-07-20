namespace LegendsTeamVN.Core.Utilities.Options;

public record ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    public string? Database { get; set; }
    public string? MigrationsAssembly { get; set; }
    public int? CommandTimeout { get; set; }
}
