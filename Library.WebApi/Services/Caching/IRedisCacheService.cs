namespace Library.WebApi.Services.Caching;

public interface IRedisCacheService
{
    Task<T?> GetDataAsync<T>(string key, CancellationToken ct = default);
    Task RemoveDataAsync(string key, CancellationToken ct = default);
    Task SetDataAsync<T>(string key, T data, CancellationToken ct = default);
}
