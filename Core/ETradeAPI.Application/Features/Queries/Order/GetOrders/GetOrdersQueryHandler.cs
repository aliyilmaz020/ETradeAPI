using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetOrders
{
    public class GetOrdersQueryHandler(
        IOrderReadRepository repository, 
        IMapper mapper) : IRequestHandler
            <GetOrdersQueryRequest, IEnumerable<GetOrdersQueryResponse>>
    {
        public Task<IEnumerable<GetOrdersQueryResponse>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = repository.GetAllWithCustomer().Skip(request.Page * request.Size).Take(request.Size);
            var mappedOrders = mapper.Map<IEnumerable<GetOrdersQueryResponse>>(orders);
            return Task.FromResult(mappedOrders);
        }
    }
}
