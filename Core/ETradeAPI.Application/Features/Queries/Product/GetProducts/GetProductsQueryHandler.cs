using AutoMapper;
using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Product.GetProducts
{
    public class GetProductsQueryHandler(
        IProductReadRepository productReadRepository, 
        IMapper mapper) : IRequestHandler
        <GetProductsQueryRequest, IEnumerable<GetProductsQueryResponse>>
    {
        public async Task<IEnumerable<GetProductsQueryResponse>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            var mappedProducts = mapper.Map<IEnumerable<GetProductsQueryResponse>>(products);
            return mappedProducts;
        }
    }
}
