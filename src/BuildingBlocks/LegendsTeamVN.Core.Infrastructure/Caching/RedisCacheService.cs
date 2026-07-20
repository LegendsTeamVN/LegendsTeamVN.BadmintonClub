using System.Text;
using LegendsTeamVN.Core.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace LegendsTeamVN.Core.Infrastructure.Caching;

public sealed class RedisCacheService(IDistributedCache distributedCache) : ICacheService
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await _distributedCache.GetAsync(key, cancellationToken);

        if (bytes is null)
        {
            return default;
        }

        string json = Encoding.UTF8.GetString(bytes);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (expiration is not null)
        {
            options.SetAbsoluteExpiration(expiration.Value);
        }

        string json = JsonConvert.SerializeObject(value);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        return _distributedCache.SetAsync(key, bytes, options, cancellationToken);
    }
}
