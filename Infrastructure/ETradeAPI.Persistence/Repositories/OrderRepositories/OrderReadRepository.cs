using ETradeAPI.Application.Repositories.OrderRepositories;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETradeAPI.Persistence.Repositories.OrderRepositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ETradeApiContext context) : base(context)
        {
        }

        public IQueryable<Order> GetAllWithCustomer()
        {
            return Table.Include(o => o.Customer);
        }

        public Order GetByIdWithCustomer(Guid id)
        {
            return Table.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
        }
    }
}
