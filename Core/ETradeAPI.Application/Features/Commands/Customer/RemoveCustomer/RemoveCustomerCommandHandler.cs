using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommandRequest, RemoveCustomerCommandResponse>
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public RemoveCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository)
        {
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<RemoveCustomerCommandResponse> Handle(RemoveCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            await _customerWriteRepository.RemoveAsync(request.Id);
            await _customerWriteRepository.SaveAsync();
            return new() { IsRemoved = true };
        }
    }
}
