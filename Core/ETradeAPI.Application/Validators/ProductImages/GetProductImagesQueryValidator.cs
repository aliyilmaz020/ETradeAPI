using ETradeAPI.Application.Features.Queries.ProductImageFile;
using FluentValidation;

namespace ETradeAPI.Application.Validators.ProductImages
{
    public class GetProductImagesQueryValidator : AbstractValidator<GetProductImagesQueryRequest>
    {
        public GetProductImagesQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Lütfen Id giriniz.").NotNull().WithMessage("Lütfen Id giriniz.");
        }
    }
}
