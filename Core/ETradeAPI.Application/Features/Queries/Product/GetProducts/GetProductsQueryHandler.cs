using AutoMapper;
using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Product.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, IEnumerable<GetProductsQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductsQueryResponse>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = _productReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            var mappedProducts = _mapper.Map<IEnumerable<GetProductsQueryResponse>>(products);
            return mappedProducts;
        }
    }
}
