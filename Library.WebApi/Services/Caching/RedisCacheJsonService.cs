// Ignore Spelling: Redis

namespace Library.WebApi.Services.Caching;

public class RedisCacheJSONService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheJSONService(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<T?> GetDataAsync<T>(string key, CancellationToken ct = default)
    {
        var data = await _cache.GetStringAsync(key, ct);

        return data == null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetDataAsync<T>(string key, T data, CancellationToken ct = default)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
        };

        await _cache.SetStringAsync(key, JsonSerializer.Serialize(data), cacheOptions, ct);
    }

    public async Task RemoveDataAsync(string key, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(key, ct);
    }
}
