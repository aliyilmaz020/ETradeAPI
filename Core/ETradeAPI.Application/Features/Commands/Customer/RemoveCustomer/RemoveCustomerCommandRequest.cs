using MediatR;

namespace ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandRequest : IRequest<RemoveCustomerCommandResponse>
    {
        public string Id { get; set; }
    }
}
