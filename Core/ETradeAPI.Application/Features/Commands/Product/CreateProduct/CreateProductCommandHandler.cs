using AutoMapper;
using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;
using P = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IMapper mapper)
        {
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<P.Product>(request);
            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            return new() { IsSuccess = true };
        }
    }
}
