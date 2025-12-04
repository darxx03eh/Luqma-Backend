using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int ItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public int CashierId { get; set; }
        public double Quantity { get; set; }
    }
}
