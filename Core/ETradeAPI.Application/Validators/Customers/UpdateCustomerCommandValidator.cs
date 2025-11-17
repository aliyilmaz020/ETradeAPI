using ETradeAPI.Application.Features.Commands.Customer.UpdateCustomer;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Customers
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommandRequest>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Lütfen müşteri Id'sini giriniz.").NotNull().WithMessage("Lütfen müşteri Id'sini giriniz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen müşteri adını giriniz.").NotNull().WithMessage("Lütfen müşteri adını giriniz.")
               .MinimumLength(2).WithMessage("Müşteri adı en az 2 karakter olmalıdır.").MaximumLength(30).WithMessage("Müşteri adı en fazla 30 karakter olmalıdır.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen müşteri soyadını giriniz.").NotNull().WithMessage("Lütfen müşteri soyadını giriniz.")
                .MinimumLength(2).WithMessage("Müşteri soyadı en az 2 karakter olmalıdır.").MaximumLength(30).WithMessage("Müşteri soyadı en fazla 30 karakter olmalıdır.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen müşteri emailini giriniz.").NotNull().WithMessage("Lütfen müşteri emailini giriniz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen müşteri telefon numarasını giriniz.").NotNull().WithMessage("Lütfen müşteri telefon numarasını giriniz.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Lütfen geçerli bir telefon numarası giriniz.");
        }
    }
}
