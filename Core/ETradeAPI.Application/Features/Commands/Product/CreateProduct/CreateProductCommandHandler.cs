using AutoMapper;
using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;
using P = ETradeAPI.Domain.Entities;

namespace ETradeAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductWriteRepository productWriteRepository,
        IMapper mapper)
        : IRequestHandler<CreateProductCommandRequest,
            CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<P.Product>(request);
            await productWriteRepository.AddAsync(product);
            await productWriteRepository.SaveAsync();
            return new() { IsSuccess = true };
        }
    }
}
