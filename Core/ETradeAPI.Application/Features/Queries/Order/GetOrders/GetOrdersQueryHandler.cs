using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, IEnumerable<GetOrdersQueryResponse>>
    {
        private readonly IOrderReadRepository _repository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderReadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<GetOrdersQueryResponse>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = _repository.GetAllWithCustomer().Skip(request.Page * request.Size).Take(request.Size);
            var mappedOrders = _mapper.Map<IEnumerable<GetOrdersQueryResponse>>(orders);
            return Task.FromResult(mappedOrders);
        }
    }
}
