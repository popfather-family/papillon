using Papillon.Storage.Services;

namespace Papillon.Storage;

public interface IStorage : ISaveService, IReadService, IDeleteService, ICacheService
{
}