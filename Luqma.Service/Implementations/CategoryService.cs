using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Trunking.V1;

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
        public async Task<string>UpdateAsync(int id,string title)
        {
         var category=  await  _categoryRepository.GetByIdAsync(id);
            if (category is null) return "the category id is not found";
            category.Title = title;
          var count=  await _categoryRepository.UpdateAsync(category);
            return count > 0 ? "the category is updated successfully" : "the category is not updated";
        }
        public async Task<string> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null) return "the category id is not found";

           var count= await _categoryRepository.DeleteAsync(category);
            return count>0? "the category is deleted successfully": "the category is not deleted";
        }
        public async Task<(Category?,string)>GetByIdAsync(int id)
        {
           var category= await _categoryRepository.GetByIdAsync(id);
            return category is null ? (null,"the category is not found") : (category,"the category is fetched successfully");
        }
    }
}
