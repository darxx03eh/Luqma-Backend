using FluentValidation;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.Features.CategoryItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.CategoryItems.Queries.validators
{
    public class GetItemsbyCategoryQueryValidator: AbstractValidator<GetItemsbyCategoryIdQuery>
    {
    
        private readonly ICategoryRepository _categoryRepository;

        public GetItemsbyCategoryQueryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            ApplyCustomValidationRules();


        }
        public void ApplyCustomValidationRules()
        {

            RuleFor(c => c.Id)
            .MustAsync(async (key, cancellation) =>
            {
                return await _categoryRepository.IsIdExistAsync(key);

            }).WithMessage(SharedResponseKeys.CategoryIdNotFound);
        }
    }
}

