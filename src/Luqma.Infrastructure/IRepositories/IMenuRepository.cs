using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
   public interface IMenuRepository:IGenericRepository<Menu>
    {
        public Task<bool> IsIdInMenuAsync(ICollection<int> MenuId);
        public Task<bool> IsIdExistInMenuAsync(int id);
        
        }
}
