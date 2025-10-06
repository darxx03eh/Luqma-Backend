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
   public class DeleteMenuValidator:AbstractValidator<DeleteMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuValidator(IMenuRepository menuRepository)
        {
            ApplyCustomValidationRules();
            _menuRepository = menuRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(dmc => dmc.Id)
                .MustAsync(async (key, cancellation) =>
                {
                    return await _menuRepository.IsIdExistInMenuAsync(key);
                }).WithMessage(SharedResponseKeys.NotFoundMenuId);
        }

    }
}
