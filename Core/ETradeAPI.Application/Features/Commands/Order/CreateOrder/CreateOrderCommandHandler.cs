using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IMapper mapper) : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<P.Order>(request);
            await orderWriteRepository.AddAsync(order);
            await orderWriteRepository.SaveAsync();
            return new()
            {
                IsCreated = true
            };
        }
    }
}
