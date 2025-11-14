using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandRequest : IRequest<UpdateOrderCommandResponse>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
