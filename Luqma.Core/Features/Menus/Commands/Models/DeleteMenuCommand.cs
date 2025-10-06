using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Menus.Commands.Models
{
   public class DeleteMenuCommand:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteMenuCommand(int id)
        {
            Id = id;
        }

    }
}
