using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Repositories.OrderRepositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
        Order GetByIdWithCustomer(Guid id);
        IQueryable<Order> GetAllWithCustomer();
    }
}
