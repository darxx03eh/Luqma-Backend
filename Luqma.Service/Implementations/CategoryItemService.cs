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
   public class CategoryItemService:ICategoryItemService
    {
        private readonly ICategoryItemRepository _categoryItemRepository;

        public CategoryItemService(ICategoryItemRepository categoryItemRepository)
        {
            _categoryItemRepository = categoryItemRepository;
        }
        public async Task<(IQueryable<CategoryItem>?,string)> GetItemsByCategoryIdAsync(int id)
        {
            var categoryitems=await _categoryItemRepository.GetItemsByCategoryIdAsync(id);
            if (!categoryitems.Any())
            {
                return (null,"the category id is not found");
            }
            return (categoryitems, "the items for category is viewed successfully");



        }
    }
}
