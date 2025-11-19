using ETradeAPI.Application.Repositories.OrderRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.RemoveOrder
{
    public class RemoveOrderCommandHandler(
        IOrderWriteRepository orderWriteRepository
        ) : IRequestHandler<RemoveOrderCommandRequest, RemoveOrderCommandResponse>
    {
        public async Task<RemoveOrderCommandResponse> Handle(RemoveOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await orderWriteRepository.RemoveAsync(request.Id);
            await orderWriteRepository.SaveAsync();
            return new()
            {
                IsRemoved = true
            };
        }
    }
}
