using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Categories.Commands.Models
{
    public class AddCategoryCommand:IRequest<ApiResponse>
    {
        public string Title { get; set; }
    }
}
