using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Iam.V1;

namespace Luqma.Core.Features.MenuContains.Queries.Models
{
    public class GetitemsbyMenuIdQuery:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetitemsbyMenuIdQuery(int id)
        {
            Id = id;
        }
    }
}
