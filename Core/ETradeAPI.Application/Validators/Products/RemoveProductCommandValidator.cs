using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Products
{
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommandRequest>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Lütfen ürün Id'sini giriniz.").NotNull().WithMessage("Lütfen ürün Id'sini giriniz.");
        }
    }
}
