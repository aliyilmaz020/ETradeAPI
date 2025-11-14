using AutoMapper;
using ETradeAPI.Application.Repositories.OrderRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IMapper mapper)
        {
            _orderWriteRepository = orderWriteRepository;
            _mapper = mapper;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<P.Order>(request);
            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();
            return new()
            {
                IsCreated = true
            };
        }
    }
}
