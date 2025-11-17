using ETradeAPI.Application.Features.Commands.Order.RemoveOrder;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Orders
{
    public class RemoveOrderCommandValidator : AbstractValidator<RemoveOrderCommandRequest>
    {
        public RemoveOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Lütfen sipariş Id'sini giriniz.").NotNull().WithMessage("Lütfen sipariş Id'sini giriniz.");
        }
    }
}
