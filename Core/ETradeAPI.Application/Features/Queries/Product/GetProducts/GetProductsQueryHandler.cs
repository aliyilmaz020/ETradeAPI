using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Product.GetProducts
{
    public class GetProductsQueryHandler(
        IProductReadRepository productReadRepository) : IRequestHandler
        <GetProductsQueryRequest, GetProductsQueryResponse>
    {
        public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var count = productReadRepository.GetAll(false).Count();
            var products = productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Price,
                    x.Stock,
                    x.CreatedDate,
                    x.UpdatedDate
                });
            return new()
            {
                TotalCount = count,
                Products = products
            };
        }
    }
}
