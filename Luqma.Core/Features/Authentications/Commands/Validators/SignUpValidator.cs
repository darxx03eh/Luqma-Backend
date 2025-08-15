using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;
        public SignUpValidator(UserManager<LuqmaUser> userManager)
        {
            this.userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage(SharedResponseKeys.FirstNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.FirstNameNotNull);

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage(SharedResponseKeys.LastNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.LastNameNotNull);

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(SharedResponseKeys.EmailNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.EmailNotNull)
                .EmailAddress().WithMessage(SharedResponseKeys.NotValidEmail);

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage(SharedResponseKeys.UserNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.UserNameNotNull);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(SharedResponseKeys.PasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.PasswordMinimumLength);

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage(SharedResponseKeys.ConfirmPasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.ConfirmPasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.ConfirmPasswordMinimumLength)
                .Equal(user => user.Password).WithMessage(SharedResponseKeys.ConfirmPasswordMustEqualToPassword);

            RuleFor(user => user.Gender)
                .IsInEnum().WithMessage(SharedResponseKeys.InvalidGender);

            RuleFor(user => user.BirthDate)
                .LessThan(DateTime.Now).WithMessage(SharedResponseKeys.BirthDateMustBeInThePast)
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage(SharedResponseKeys.BirthDateIsUnrealistic);

            RuleFor(user => user.Salary)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.SalaryMustBePositive);

            RuleFor(user => user.Role)
                .NotEmpty().WithMessage(SharedResponseKeys.RoleNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.RoleNotNull);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserName)
                .MustAsync(async (key, cancellation) =>
                {
                    var username = await userManager.FindByNameAsync(key);
                    return username is null;
                }).WithMessage(SharedResponseKeys.UserNameAlreadyExist);
            RuleFor(user => user.Email)
                .MustAsync(async (key, cancellation) =>
                {
                    var email = await userManager.FindByEmailAsync(key);
                    return email is null;
                }).WithMessage(SharedResponseKeys.EmailAlreadyExist);
        }
    }
}
