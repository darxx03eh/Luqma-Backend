using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Bills.Commands.Models
{
    public class UpdateBillCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public string? Note { get; set; }
        public double TotalPrice { get; set; }
    }
}
