using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.CategoryItems.Queries.Models
{
   public  class GetItemsbyCategoryIdQuery :IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetItemsbyCategoryIdQuery(int id)
        {
            Id = id;
        }

    }
}
