using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.Core.Application.Caching;

public interface ICachedQuery<TResponse> : IQuery<TResponse>
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}
