using Luqma.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface IMenuItemService
    {
        public Task<string> AddMenuItemAsync(MenuItem menuItem, IFormFile file, ICollection<int> CategoryId, ICollection<int> MenuId);
        public Task<string> DeleteMenuItemAsync(MenuItem menuItem);
        public Task<string> UpdateMenuItemAsync(MenuItem menuItem, IFormFile file);
        public  Task<(IQueryable<MenuItem>?, string)> GetAllAsync();


    }
}
