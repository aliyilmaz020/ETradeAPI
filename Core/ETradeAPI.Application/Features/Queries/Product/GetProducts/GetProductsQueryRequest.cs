using MediatR;

namespace ETradeAPI.Application.Features.Queries.Product.GetProducts
{
    public class GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
    {
        public int Page { get; init; } = 0;
        public int Size { get; init; } = 5;
    }
}
