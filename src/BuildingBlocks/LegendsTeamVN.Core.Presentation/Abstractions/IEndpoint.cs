using Microsoft.AspNetCore.Routing;

namespace LegendsTeamVN.Core.Presentation.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
