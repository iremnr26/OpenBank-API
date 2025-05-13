using System;
using FluentValidation;
using OpenBankAPI.Application.ViewModels.Accounts;

namespace OpenBankAPI.Application.Validators.Accounts
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Account>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("Please enter the UserID.")
                .NotNull().WithMessage("UserID cannot be null.")
                .Must(userId => int.TryParse(userId, out int id) && id > 0)
                .WithMessage("UserID must be a positive number greater than zero.");

            RuleFor(p => p.BankId)
                .NotEmpty().WithMessage("Please select a Bank.")
                .NotNull().WithMessage("Bank selection is required.")
                .Must(bankId => bankId != "0").WithMessage("Please select a valid Bank.");

            RuleFor(p => p.Currency)
                .NotEmpty().WithMessage("Please select a Currency.")
                .NotNull().WithMessage("Currency selection is required.")
                .Must(currency => currency != "0").WithMessage("Please select a valid Currency.");
        }
    }
}
