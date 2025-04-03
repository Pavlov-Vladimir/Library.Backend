namespace Library.WebApi.Services.Caching;

public class RedisCacheMPService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheMPService(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<T?> GetDataAsync<T>(string key, CancellationToken ct = default)
    {
        var data = await _cache.GetAsync(key, ct);

        return data == null ? default : MessagePackSerializer.Deserialize<T>(data);
    }

    public async Task SetDataAsync<T>(string key, T data, CancellationToken ct = default)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
        };

        await _cache.SetAsync(key, MessagePackSerializer.Serialize(data), cacheOptions, ct);
    }

    public async Task RemoveDataAsync(string key, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(key, ct);
    }
}
