using ETradeAPI.Application.Repositories;
using ETradeAPI.Domain.Entities.Common;
using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace ETradeAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETradeApiContext _context;

        public WriteRepository(ETradeApiContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> CreateAsync(T model)
        {
            EntityEntry entry = await Table.AddAsync(model);
            return entry.State == EntityState.Added;
        }

        public async Task<bool> CreateRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return Remove(model);
        }

        public bool Remove(T model)
        {
            EntityEntry entry = Table.Remove(model);
            return entry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T model)
        {
            EntityEntry entry = Table.Update(model);
            return entry.State == EntityState.Modified;
        }
    }
}
