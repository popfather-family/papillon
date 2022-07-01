namespace Papillon.Storage.Services;

public interface IReadService
{
    Task<TValue> FindAsync<TValue>(Key key);

    Task<TValue?> FindOrDefaultAsync<TValue>(Key key);

    Task<bool> AnyAsync(Key key);
}