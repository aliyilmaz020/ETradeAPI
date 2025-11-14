using AutoMapper;
using ETradeAPI.Application.Repositories.CustomerRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IMapper mapper, ICustomerReadRepository customerReadRepository)
        {
            _customerWriteRepository = customerWriteRepository;
            _mapper = mapper;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerReadRepository.GetByIdAsync(request.Id.ToString());
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveAsync();
            return new() { IsSucces = true };

        }
    }
}
