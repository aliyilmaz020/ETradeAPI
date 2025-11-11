using ETradeAPI.Domain.Entities.Common;

namespace ETradeAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> CreateAsync(T model);
        Task<bool> CreateRangeAsync(List<T> models);
        bool Update(T model);
        Task<bool> RemoveAsync(string id);
        bool Remove(T model);
        bool RemoveRange(List<T> models);
        Task<int> SaveAsync();
    }
}
