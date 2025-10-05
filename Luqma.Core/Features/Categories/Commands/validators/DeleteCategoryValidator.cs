using FluentValidation;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Categories.Commands.validators
{
   public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryValidator(ICategoryRepository categoryRepository)
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
