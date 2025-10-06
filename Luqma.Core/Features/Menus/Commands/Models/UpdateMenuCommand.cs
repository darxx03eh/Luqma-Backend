using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Menus.Commands.Models
{
   public class UpdateMenuCommand :IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
