using ETradeAPI.Application.Features.Commands.Order.CreateOrder;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Orders
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommandRequest>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Lütfen sipariş açıklamasını giriniz.")
                .NotNull().WithMessage("Lütfen sipariş açıklamasını giriniz.")
               .MinimumLength(5)
               .WithMessage("Sipariş açıklaması en az 5 karakter olmalıdır.")
               .MaximumLength(500)
               .WithMessage("Sipariş açıklaması en fazla 500 karakter olmalıdır.");
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Lütfen müşteri Id'sini giriniz.")
                .NotNull().WithMessage("Lütfen müşteri Id'sini giriniz.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Lütfen sipariş adresini giriniz.")
                .NotNull().WithMessage("Lütfen sipariş adresini giriniz.")
               .MinimumLength(10)
               .WithMessage("Sipariş adresi en az 10 karakter olmalıdır.")
               .MaximumLength(300)
               .WithMessage("Sipariş adresi en fazla 300 karakter olmalıdır.");
        }
    }
}
