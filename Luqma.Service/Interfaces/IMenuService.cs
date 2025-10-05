using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
   public interface IMenuService
    {
        public  Task<string> AddMenuAsync(Menu menu);
        public  Task<(IQueryable<Menu>?, string)> GetAllMenusAsync();
    }
}
