using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Response.MenuItems
{
   public  class MenuItemResponse
    {

        public int Id { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
        public Status Status { get; set; }
    }
}
