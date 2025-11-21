using ETradeAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETradeAPI.Application.Features.Queries.ProductImageFile
{
    public class GetProductImagesQueryHandler(
        IProductReadRepository productReadRepository,
        IConfiguration configuration
        ) : IRequestHandler<GetProductImagesQueryRequest, IEnumerable<GetProductImagesQueryResponse>>
    {
        public async Task<IEnumerable<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            var value = await productReadRepository.Table.Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            if (value == null)
            {
                return [];
            }
            return value.ProductImageFiles.Select(x => new GetProductImagesQueryResponse
            {
                Id = x.Id,
                Path = $"{configuration["Storage:BaseStorageUrl"]}{x.Path}",
                FileName = x.FileName
            });
        }
    }
}
