using FluentValidation;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Commands.Validators
{
    public class DeleteMenuItemValidator:AbstractValidator<DeleteMenuItemCommand>
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public DeleteMenuItemValidator(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            ApplyCustomValidationRules();
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(dmi=>dmi.Id)
            .MustAsync(async (key, cancellation) =>
            {
                return await _menuItemRepository.IsIdExistAsync(key);

                 }).WithMessage(SharedResponseKeys.NotFounItemId);

        }
    }
}
