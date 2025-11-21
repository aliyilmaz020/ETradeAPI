using ETradeAPI.Application.Abstractions.Storage;
using ETradeAPI.Application.Repositories.ProductImageFileRepositories;
using MediatR;

namespace ETradeAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler(
        IProductImageFileReadRepository productImageFileReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IStorageService storageService
        ) : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var imageFile = await productImageFileReadRepository.GetByIdAsync(request.Id);
            if (imageFile == null)
                return null;
            await storageService.DeleteAsync("product-images", imageFile.FileName);
            productImageFileWriteRepository.Remove(imageFile);
            await productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
