using ETradeAPI.Application.Abstractions;
using ETradeAPI.Domain.Entities;

namespace ETradeAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        => new()
        {
            new(){Id = Guid.NewGuid(),Name = "P1",Price=1000,Stock=10},
            new(){Id = Guid.NewGuid(),Name = "P2",Price=2000,Stock=20},
            new(){Id = Guid.NewGuid(),Name = "P3",Price=3000,Stock=30},
            new(){Id = Guid.NewGuid(),Name = "P4",Price=4000,Stock=40}
        };
    }
}
