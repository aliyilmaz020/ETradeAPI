using ETradeAPI.Application.Repositories.ProductRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;
using AutoMapper;

namespace ETradeAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            if (product == null)
                return null!;
            var mappedProduct = _mapper.Map<GetByIdProductQueryResponse>(request);
            return mappedProduct;
        }
    }
}
