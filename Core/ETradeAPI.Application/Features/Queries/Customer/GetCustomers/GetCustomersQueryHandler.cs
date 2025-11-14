using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Queries.Customer.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQueryRequest, IEnumerable<GetCustomersQueryResponse>>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerReadRepository customerReadRepository, IMapper mapper)
        {
            _customerReadRepository = customerReadRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<GetCustomersQueryResponse>> Handle(GetCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            var customers = _customerReadRepository.GetAll(false).Skip(request.Size * request.Page).Take(request.Size);
            var response = _mapper.Map<IEnumerable<GetCustomersQueryResponse>>(customers);
            return Task.FromResult(response);
        }
    }
}
