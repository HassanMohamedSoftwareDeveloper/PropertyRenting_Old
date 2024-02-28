using Microsoft.Extensions.Caching.Memory;
using PropertyRenting.Api.Helpers;
using System.Collections.Concurrent;

namespace PropertyRenting.Api.Services;

public class InMemoryCacheService : ICacheService
{
    #region Fields :

    private readonly IMemoryCache _memoryCache;
    private static readonly ConcurrentDictionary<string, bool> CachKeys = new();
    private readonly string _tenant;

    #endregion Fields :

    #region CTORS :

    public InMemoryCacheService(IMemoryCache memoryCache, IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _tenant = configuration["TenantName"];
    }

    #endregion CTORS :

    #region Methods :

    public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedValue = _memoryCache.Get<T>($"{_tenant}_{Localizable.CurrentCultureName}-{key}");
        return Task.FromResult(cachedValue);
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int expirationInMinutes, CancellationToken cancellationToken = default)
    {
        //CachKeys.TryAdd(key, false);
        return await factory();
        //return await _memoryCache.GetOrCreateAsync<T>($"{_tenant}_{Localizable.CurrentCultureName}-{key}",
        //      entry =>
        //      {
        //          entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationInMinutes));
        //          return factory();
        //      });
    }

    public Task SetAsync<T>(string key, T value, int expirationInMinutes, CancellationToken cancellationToken = default)
    {
        if (value is not null)
        {
            _memoryCache.Set($"{_tenant}_{Localizable.CurrentCultureName}-{key}", value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes) });
            CachKeys.TryAdd(key, false);
        }

        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _memoryCache.Remove($"{_tenant}_{Constants.Constants.Language.ArabicLanguageCode}-{key}");
        _memoryCache.Remove($"{_tenant}_{Constants.Constants.Language.EnglishLanguageCode}-{key}");

        CachKeys.TryRemove(key, out bool _);
        return Task.CompletedTask;
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        //prefixKey = $"{_tenant}_{Localizable.CurrentCultureName}-{prefixKey}";
        //var tasks = CachKeys.Keys
        //    .Where(k => k.StartsWith(prefixKey))
        //    .Select(k => RemoveAsync(k, cancellationToken));

        //await Task.WhenAll(tasks);
    }

    #endregion Methods :
}