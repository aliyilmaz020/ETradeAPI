using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetByIdCustomer
{
    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQueryRequest, GetByIdCustomerQueryResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IMapper _mapper;

        public GetByIdCustomerQueryHandler(ICustomerReadRepository customerReadRepository, IMapper mapper)
        {
            _customerReadRepository = customerReadRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdCustomerQueryResponse> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<GetByIdCustomerQueryResponse>(await _customerReadRepository.GetByIdAsync(request.Id,false));
            return customer;
        }
    }
}
