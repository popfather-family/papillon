namespace Papillon.Storage.Services;

public interface IDeleteService
{
    Task DeleteAsync(Key key);
}