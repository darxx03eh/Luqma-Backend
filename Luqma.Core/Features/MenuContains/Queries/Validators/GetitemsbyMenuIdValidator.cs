using FluentValidation;
using Luqma.Core.Features.MenuContains.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuContains.Queries.Validators
{
    public class GetitemsbyMenuIdValidator:AbstractValidator<GetitemsbyMenuIdQuery>
    {
        private readonly IMenuRepository _menuRepository;

        public GetitemsbyMenuIdValidator(IMenuRepository menuRepository)
        {
            ApplyCustomValidationRules();
            _menuRepository = menuRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(m=>m.Id)
            .MustAsync(async (key, cancellation) =>
             {
                 return await _menuRepository.IsIdExistInMenuAsync(key);

             }).WithMessage(SharedResponseKeys.NotFoundMenuId);
        }

    }
}
