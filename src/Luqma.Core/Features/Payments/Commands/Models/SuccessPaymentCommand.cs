using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Payments.Commands.Models
{
    public class SuccessPaymentCommand:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public SuccessPaymentCommand(int id)
        {
            Id = id;
        }



    }
}
