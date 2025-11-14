using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IMapper _mapper;

        public GetByIdOrderQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }

        public Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<GetByIdOrderQueryResponse>(_orderReadRepository.GetByIdWithCustomer(Guid.Parse(request.Id)));
            return Task.FromResult(order);
        }
    }
}
