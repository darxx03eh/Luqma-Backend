using FluentValidation;
using Luqma.Core.Features.MenuItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Queries.Validators
{
    public class GetByIdValidator :AbstractValidator<GetMenuItemByIdQuery>
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public GetByIdValidator(IMenuItemRepository menuItemRepository)
        {
            ApplyCustomValidationRules();
            _menuItemRepository = menuItemRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(dmi => dmi.Id)
            .MustAsync(async (key, cancellation) =>
            {
                return await _menuItemRepository.IsIdExistAsync(key);

            }).WithMessage(SharedResponseKeys.NotFounItemId);

        }
    }
    }

