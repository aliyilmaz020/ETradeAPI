using ETradeAPI.Application.Features.Commands.Customer.RemoveCustomer;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Customers
{
    public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommandRequest>
    {
        public RemoveCustomerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Lütfen müşteri Id'sini giriniz.").NotNull().WithMessage("Lütfen müşteri Id'sini giriniz.");
        }
    }
}
