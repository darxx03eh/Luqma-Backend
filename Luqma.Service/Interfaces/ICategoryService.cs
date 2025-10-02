using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface ICategoryService
    {
        public Task<(ICollection<Category>?, string)> GetAllAsync();
        public Task<string> CreateAsync(Category category);
        public  Task<string> UpdateAsync(Category category);
       public  Task<string> DeleteAsync(Category category);
        public Task<(Category?, string)> GetByIdAsync(int id);
    }
}
