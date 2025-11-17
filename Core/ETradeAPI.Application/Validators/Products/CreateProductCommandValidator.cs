using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen ürün adını giriniz.").NotNull().WithMessage("Lütfen ürün adını giriniz.")
                .MinimumLength(3).WithMessage("Ürün Adı en az 3 karakter olmalıdır.").MaximumLength(150).WithMessage("Ürün Adı en fazla 150 karakter olmalıdır.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Lütfen stok sayısını giriniz.").NotNull().WithMessage("Lütfen stok sayısını giriniz.")
                .Must(s => s >= 0).WithMessage("Stok sayısı negatif olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Lütfen ürün fiyatını giriniz.").NotNull().WithMessage("Lütfen ürün fiyatını giriniz.")
                .Must(s => s >= 0).WithMessage("Ürün fiyatı negatif olamaz");
        }
    }
}
