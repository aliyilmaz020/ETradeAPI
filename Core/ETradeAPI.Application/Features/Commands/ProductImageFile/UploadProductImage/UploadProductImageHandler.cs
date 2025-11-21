using ETradeAPI.Application.Abstractions.Storage;
using ETradeAPI.Application.Repositories.ProductImageFileRepositories;
using ETradeAPI.Application.Repositories.ProductRepositories;
using P = ETradeAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageHandler(
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IProductReadRepository productReadRepository,
        IStorageService storageService,
        IHttpContextAccessor accessor
        )
        : IRequestHandler<UploadProductImageCommandRequest,
            UploadProductImageCommandResponse>
    {
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            request.Files = accessor.HttpContext.Request.Form.Files;
            var product = await productReadRepository.GetByIdAsync(request.Id);
            List<(string fileName, string path)> images =
                await storageService.UploadAsync("product-images", accessor.HttpContext.Request.Form.Files);

            await productImageFileWriteRepository.AddRangeAsync(images.Select(i => new P.ProductImageFile
            {
                FileName = i.fileName,
                Path = i.path,
                Storage = storageService.StorageName,
                Products = new List<P.Product> { product }
            }).ToList());
            await productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
