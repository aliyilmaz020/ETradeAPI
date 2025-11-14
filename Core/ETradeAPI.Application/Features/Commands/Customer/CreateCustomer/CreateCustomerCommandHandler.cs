using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IMapper mapper)
        {
            _customerWriteRepository = customerWriteRepository;
            _mapper = mapper;
        }

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<P.Customer>(request);
            await _customerWriteRepository.AddAsync(customer);
            await _customerWriteRepository.SaveAsync();
            return new();
        }
    }
}
