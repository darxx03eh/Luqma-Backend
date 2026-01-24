using Luqma.Core.Bases;
using Luqma.Core.Features.Bills.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Bills.Queries.Handlers
{
    public class BillQueryHandler : ApiResponseHandler
        , IRequestHandler<GetBillsQuery, ApiResponse>
    {
        private readonly IBillService billService;

        public BillQueryHandler(IBillService billService)
        {
            this.billService = billService;
        }

        public async Task<ApiResponse> Handle(GetBillsQuery request, CancellationToken cancellationToken)
        {
            var (result, bills) = await billService.GetBillsAsync(request.PageNumber);
            return result switch
            {
                "BillsNotFound" => NotFound(SharedResponseKeys.BillsNotFound),
                "BillsFound" => Success(bills, message: SharedResponseKeys.BillsFound),
                _ => NotFound(SharedResponseKeys.BillsNotFound)
            };
        }
    }
}
