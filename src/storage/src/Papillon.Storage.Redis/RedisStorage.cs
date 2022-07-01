using Foundatio.Caching;

namespace Papillon.Storage.Redis;

[Singleton]
public class RedisStorage : IStorage
{
    private readonly ICacheClient client;

    public RedisStorage(ICacheClient client)
    {
        this.client = client;
    }

    public Task PutAsync<TValue>(Key key, TValue value)
    {
        return client.SetAsync(key.Value, value);
    }

    public Task CacheAsync<TValue>(CacheKey key, TValue value)
    {
        var expireTime = ComputeExpiryTimeSpan(key);

        return client.SetAsync(key.Value, value, expireTime);
    }

    public Task DeleteAsync(Key key)
    {
        return client.RemoveAsync(key.Value);
    }

    public async Task<TValue> FindAsync<TValue>(Key key)
    {
        var value = await FindOrDefaultAsync<TValue>(key);
        if (value is null)
        {
            throw new KeyNotFoundException(key.Value);
        }

        return value;
    }

    public async Task<TValue?> FindOrDefaultAsync<TValue>(Key key)
    {
        var cacheValue = await client.GetAsync<TValue>(key.Value);

        return cacheValue.Value;
    }

    public Task<bool> AnyAsync(Key key) => client.ExistsAsync(key.Value);

    private static TimeSpan? ComputeExpiryTimeSpan(CacheKey key) => TimeSpan.FromSeconds(key.ExpirySeconds);
}