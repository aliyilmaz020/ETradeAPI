using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetOrders
{
    public class GetOrdersQueryRequest : IRequest<IEnumerable<GetOrdersQueryResponse>>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
