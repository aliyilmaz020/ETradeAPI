using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler(
        IOrderWriteRepository orderWriteRepository,
        IOrderReadRepository orderReadRepository) 
        : IRequestHandler<UpdateOrderCommandRequest, 
            UpdateOrderCommandResponse>
    {
        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await orderReadRepository.GetByIdAsync(request.Id.ToString());
            order.Description = request.Description;
            order.Address = request.Address;
            if (request.CustomerId.HasValue)
                order.CustomerId = request.CustomerId.Value;
            orderWriteRepository.Update(order);
            await orderWriteRepository.SaveAsync();
            return new()
            {
                IsUpdated = true
            };
        }
    }
}
