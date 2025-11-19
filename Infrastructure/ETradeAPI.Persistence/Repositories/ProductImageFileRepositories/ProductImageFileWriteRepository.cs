using ETradeAPI.Application.Repositories.ProductImageFileRepositories;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistence.Contexts;

namespace ETradeAPI.Persistence.Repositories.ProductImageFileRepositories
{
    public class ProductImageFileWriteRepository(ETradeApiContext context)
        : WriteRepository<ProductImageFile>(context),
            IProductImageFileWriteRepository
    {
    }
}
