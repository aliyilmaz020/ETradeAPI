using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetCustomers
{
    public class GetCustomersQueryHandler(
        ICustomerReadRepository customerReadRepository,
        IMapper mapper) :
        IRequestHandler<GetCustomersQueryRequest,
            IEnumerable<GetCustomersQueryResponse>>
    {
        public Task<IEnumerable<GetCustomersQueryResponse>> Handle(GetCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            var customers = customerReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            var response = mapper.Map<IEnumerable<GetCustomersQueryResponse>>(customers);
            return Task.FromResult(response);
        }
    }
}
