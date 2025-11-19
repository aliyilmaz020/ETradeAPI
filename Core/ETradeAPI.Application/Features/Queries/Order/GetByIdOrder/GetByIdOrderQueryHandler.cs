using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler(
        IOrderReadRepository orderReadRepository, 
        IMapper mapper) : IRequestHandler
            <GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        public Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<GetByIdOrderQueryResponse>(orderReadRepository.GetByIdWithCustomer(Guid.Parse(request.Id)));
            return Task.FromResult(order);
        }
    }
}
