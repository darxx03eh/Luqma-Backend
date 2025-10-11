using Luqma.Data.Entities;
using Luqma.Data.Response.Bills;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Luqma.Infrastructure.Repositories
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BillRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string, PaginatedResult<BillsResponse>?)> GetBillsAsync(int pageNumber)
        {
            var billsQueryable = GetTableNoTracking().OrderByDescending(bill => bill.BillDate).AsQueryable();
            if (billsQueryable is null)
                return ("BillsNotFound", null);

            var bills = await billsQueryable.Select(bill => new BillsResponse()
            {
                Id = bill.Id,
                BillType = bill.BillType,
                FinanceName = $"{bill.Finance.FirstName} {bill.Finance.LastName}",
                Amount = bill.TotalPrice,
                DueDate = bill.DueDate.Value.ToString("yyyy-MM-dd hh:mm tt") ?? "N/A",
                Status = bill.Status,
            }).ToPaginatedListAsync(pageNumber, 5);
            if (bills.Data.Count().Equals(0))
                return ("BillsNotFound", null);
            return ("BillsFound", bills);
        }
    }
}