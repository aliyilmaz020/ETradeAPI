using MediatR;

namespace ETradeAPI.Application.Features.Queries.ProductImageFile
{
    public class GetProductImagesQueryRequest : IRequest<IEnumerable<GetProductImagesQueryResponse>>
    {
        public string? Id { get; set; }
    }
}
