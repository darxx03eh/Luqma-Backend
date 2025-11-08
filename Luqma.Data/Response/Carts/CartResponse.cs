using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Response.Carts
{
    public class CartResponse
    {
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
