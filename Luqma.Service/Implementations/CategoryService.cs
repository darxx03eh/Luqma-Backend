using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<(ICollection<Category>?,string)> GetAllAsync()
        {
          var categories=  await _categoryRepository.GetAllAsync();
            if(!categories.Any())
            {
                return (null, "sorry! the categories are not found");
            }
            return (categories, "the categories are viewed successfully");
        }
        public async Task<string> CreateAsync(Category category)
        {
            if(category is null)
            {
                throw new Exception("can not add null");
            }
            await _categoryRepository.AddAsync(category);
            return "the category is created successfully";
        }
    }
}
