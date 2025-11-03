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
        public Task<string> DeleteMenuItemAsync(int id);
        public Task<string> UpdateMenuItemAsync(int id, string item, string description, double? discount, double price, bool isvegetarian, IFormFile? file, Status status);
        public  Task<(IQueryable<MenuItem>?, string)> GetAllAsync();
        public Task<(MenuItem, string)> GetByIdAsync(int id);



    }
}
