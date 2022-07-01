namespace Papillon.Storage;

public record CacheKey
{
    public CacheKey(Id id, Seconds expirySeconds)
    {
        ExpirySeconds = expirySeconds;
        Value = new Key(id);
    }

    public CacheKey(string scope, Id id, Seconds expirySeconds)
    {
        ExpirySeconds = expirySeconds;
        Value = new Key(scope, id);
    }

    public static CacheKey From(IIdentifiable identifiable, Seconds expirySeconds)
    {
        return new CacheKey(identifiable.Id, expirySeconds);
    }

    public static CacheKey From(string scope, IIdentifiable identifiable, Seconds expirySeconds)
    {
        return new CacheKey(scope, identifiable.Id, expirySeconds);
    }

    public string Value { get; }

    public Seconds ExpirySeconds { get; }
}