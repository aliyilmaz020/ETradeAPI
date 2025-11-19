using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandHandler(
        ICustomerWriteRepository customerWriteRepository,
        IMapper mapper) :
            IRequestHandler<
            CreateCustomerCommandRequest,
            CreateCustomerCommandResponse>
    {
        public async Task<CreateCustomerCommandResponse> Handle(
            CreateCustomerCommandRequest request,
            CancellationToken cancellationToken)
        {
            var customer = mapper.Map<P.Customer>(request);
            await customerWriteRepository.AddAsync(customer);
            await customerWriteRepository.SaveAsync();
            return new();
        }
    }
}
