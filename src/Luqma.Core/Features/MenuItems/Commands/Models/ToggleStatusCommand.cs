using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Commands.Models
{
    public class ToggleStatusCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public ToggleStatusCommand(int id)
        {
            Id = id;
        }
    }
}
