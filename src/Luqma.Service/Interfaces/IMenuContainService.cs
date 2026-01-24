using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface IMenuContainService
    {
        public Task<(IQueryable<MenuContains>?, string)> GetItemsByMenuIdAsync(int id);
    }
}
