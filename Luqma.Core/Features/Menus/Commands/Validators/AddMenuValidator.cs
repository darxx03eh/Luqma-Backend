using FluentValidation;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.ResponseKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Menus.Commands.Validators
{
    public class AddMenuValidator: AbstractValidator<AddMenuCommand>
    {

        public AddMenuValidator()
        {
            ApplyValidationRules();
           
        }
        public void ApplyValidationRules()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage(SharedResponseKeys.TitleNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.TitleNotNull)
                .MaximumLength(100).WithMessage(SharedResponseKeys.TitleMaximumLength);
            RuleFor(m => m.Description)
                .MaximumLength(500).WithMessage(SharedResponseKeys.DescriptionMaximumLength);


        }
    }
}
