using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Categories.Queries.Models
{
    public class GetCategoryQuery:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetCategoryQuery(int id)
        {
            Id = id;
        }
    }
}
