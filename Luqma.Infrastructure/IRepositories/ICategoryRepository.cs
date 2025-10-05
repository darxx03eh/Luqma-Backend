using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
   public interface ICategoryRepository:IGenericRepository<Category>
    {
     public Task<bool> IsTitleExistAsync(string Title);
        public Task<bool> IsIdExistInCategoryAsync(ICollection<int> Id);
        public  Task<bool> IsIdExistAsync(int id);
    }
}
