using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
   public interface ICategoryItemRepository:IGenericRepository<CategoryItem>
    {
        public Task<IQueryable<CategoryItem>> GetItemsByCategoryIdAsync(int id);
        public Task<List<CategoryItem>> GetAllCategoryItemsAsync();
    }
}
