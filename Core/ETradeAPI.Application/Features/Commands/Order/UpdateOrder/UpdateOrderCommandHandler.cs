using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public UpdateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetByIdAsync(request.Id.ToString());
            order.Description = request.Description;
            order.Address = request.Address;
            if (request.CustomerId.HasValue)
                order.CustomerId = request.CustomerId.Value;
            _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
            return new()
            {
                IsUpdated = true
            };
        }
    }
}
