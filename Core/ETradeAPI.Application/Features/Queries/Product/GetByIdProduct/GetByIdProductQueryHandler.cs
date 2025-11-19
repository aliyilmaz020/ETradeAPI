using ETradeAPI.Application.Repositories.ProductRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;
using AutoMapper;

namespace ETradeAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler(
        IProductReadRepository productReadRepository,
        IMapper mapper) : IRequestHandler
        <GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await productReadRepository.GetByIdAsync(request.Id, false);
            if (product == null)
                return null!;
            var mappedProduct = mapper.Map<GetByIdProductQueryResponse>(product);
            return mappedProduct;
        }
    }
}
