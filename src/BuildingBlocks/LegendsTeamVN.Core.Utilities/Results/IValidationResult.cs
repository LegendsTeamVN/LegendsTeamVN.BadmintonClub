namespace LegendsTeamVN.Core.Utilities.Results;

public interface IValidationResult
{
    Error[] Errors { get; }
}
