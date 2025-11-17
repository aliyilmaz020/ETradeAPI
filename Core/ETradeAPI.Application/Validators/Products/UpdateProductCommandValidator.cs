using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Products
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Lütfen ürün Id'sini giriniz.").NotNull().WithMessage("Lütfen ürün Id'sini giriniz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen ürün adını giriniz.").NotNull().WithMessage("Lütfen ürün adını giriniz.")
               .MinimumLength(3).WithMessage("Ürün Adı en az 3 karakter olmalıdır.").MaximumLength(150).WithMessage("Ürün Adı en fazla 150 karakter olmalıdır.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Lütfen stok sayısını giriniz.").NotNull().WithMessage("Lütfen stok sayısını giriniz.")
                .Must(s => s >= 0).WithMessage("Stok sayısı negatif olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Lütfen ürün fiyatını giriniz.").NotNull().WithMessage("Lütfen ürün fiyatını giriniz.")
                .Must(s => s >= 0).WithMessage("Ürün fiyatı negatif olamaz");
        }
    }
}
