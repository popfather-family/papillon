namespace Papillon.Storage.Services;

public interface ICacheService
{
    Task CacheAsync<TValue>(CacheKey key, TValue value);
}