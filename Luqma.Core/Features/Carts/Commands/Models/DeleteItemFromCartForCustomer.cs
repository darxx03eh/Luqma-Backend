using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Carts.Commands.Models
{
   public class DeleteItemFromCartForCustomer : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteItemFromCartForCustomer(int id)
        {
            Id = id;
        }
    }
}
