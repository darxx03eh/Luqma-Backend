using FluentValidation;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Menus.Commands.Validators
{
    public class UpdateMenuValidator:AbstractValidator<UpdateMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;

        public UpdateMenuValidator(IMenuRepository menuRepository)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _menuRepository = menuRepository;
        }
        public void ApplyValidationRules()
        {
            RuleFor(um => um.Title)
                .NotEmpty().WithMessage(SharedResponseKeys.TitleNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.TitleNotNull)
                .MaximumLength(100).WithMessage(SharedResponseKeys.TitleMaximumLength);
            RuleFor(um => um.Description)
                .MaximumLength(500).WithMessage(SharedResponseKeys.DescriptionMaximumLength);


        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(um => um.Id)
                .MustAsync(async (key, cancellation) =>
                {
                    return await _menuRepository.IsIdExistInMenuAsync(key);
                }).WithMessage(SharedResponseKeys.NotFoundMenuId);
        }

    }

    }

