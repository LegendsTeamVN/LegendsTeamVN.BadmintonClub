namespace LegendsTeamVN.Core.Domain.Entities;

public interface IAuditable
{
    DateTimeOffset CreatedOnUtc { get; }
    string? CreatedBy { get; }
    DateTimeOffset? ModifiedOnUtc { get; }
    string? ModifiedBy { get; }
}
