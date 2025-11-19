using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer
{
    public class GetByIdCustomerQueryHandler(
        ICustomerReadRepository customerReadRepository,
        IMapper mapper) : IRequestHandler<GetByIdCustomerQueryRequest,
            GetByIdCustomerQueryResponse>
    {
        public async Task<GetByIdCustomerQueryResponse> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<GetByIdCustomerQueryResponse>(await customerReadRepository.GetByIdAsync(request.Id, false));
            return customer;
        }
    }
}
