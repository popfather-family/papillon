namespace Papillon.Storage.Services;

public interface ISaveService
{
    Task PutAsync<TValue>(Key key, TValue value);
}