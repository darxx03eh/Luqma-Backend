using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Response.Order
{
    public class ViewOrderDetailsResponse
    {
        public string Item { get; set; }
        public double Quantity { get; set; }
        public double? TotalPrice { get; set; }
    }
}
