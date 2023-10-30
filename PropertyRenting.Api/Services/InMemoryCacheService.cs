using Microsoft.Extensions.Caching.Memory;

namespace PropertyRenting.Api.Services;

public class InMemoryCacheService : ICacheService
{
    #region Fields :
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region CTORS :
    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    #endregion

    #region Methods :
    public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedValue = _memoryCache.Get<T>(key);
        return Task.FromResult(cachedValue);
    }
    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int expirationInMinutes, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync<T>(key,
              entry =>
              {
                  entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationInMinutes));
                  return factory();
              });
    }
    public Task SetAsync<T>(string key, T value, int expirationInMinutes, CancellationToken cancellationToken = default)
    {
        if (value is not null)
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes) });

        return Task.CompletedTask;
    }
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }
    #endregion
}
