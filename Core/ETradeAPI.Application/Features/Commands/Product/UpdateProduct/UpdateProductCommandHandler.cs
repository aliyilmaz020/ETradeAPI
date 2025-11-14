using AutoMapper;
using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id.ToString());
            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return new() { Id = request.Id, IsSuccess = true };
        }
    }
}
