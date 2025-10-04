using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Deductions;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Repositories
{
    public class DeductionRepository : GenericRepository<Deduction>, IDeductionRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<LuqmaUser> userManager;

        public DeductionRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<LuqmaUser> userManager)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsByDateAsync(int pageNumber, int year, int month)
        {
            var deductionsQueryable = GetTableNoTracking().Where(
                deduction => deduction.DeductionDate.Year.Equals(year) && deduction.DeductionDate.Month.Equals(month)
                ).OrderByDescending(deduction => deduction.DeductionDate).AsQueryable();

            if (deductionsQueryable is null)
                return ("DeductionsNotFound", null);

            var deduction = await deductionsQueryable.Select(deduction => new ViewDeductionsResponse()
            {
                Id = deduction.Id,
                User = new User()
                {
                    Id = deduction.User.Id,
                    ImageUrl = deduction.User.ImageUrl,
                    Name = $"{deduction.User.FirstName} {deduction.User.LastName}",
                    Email = deduction.User.Email,
                },
                FinanceName = $"{deduction.Finance.FirstName} {deduction.Finance.LastName}",
                DeductionRate = deduction.DeductionRate,
                DeductionDate = deduction.DeductionDate.ToString("yyyy-MM-dd hh:mm tt") ?? "N/A"
            }).ToPaginatedListAsync(pageNumber, 5);
            if (deduction.Data.Count().Equals(0))
                return ("DeductionsNotFound", null);

            foreach (var user in deduction.Data)
            {
                var roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.User.Id.ToString()));
                user.User.Role = roles.FirstOrDefault() ?? "NoRole";
            }
            return ("DeductionsFound", deduction);
        }

        public async Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsForSpecificUser(int pageNumber, string name)
        {
            
            var deductionsQueryable = GetTableNoTracking().Where(
                deduction => EF.Functions.Like((deduction.User.FirstName + " " + deduction.User.LastName), $"%{name}%")
                ).OrderByDescending(deduction => deduction.DeductionDate).AsQueryable();

            if (deductionsQueryable is null)
                return ("DeductionsNotFound", null);

            var deduction = await deductionsQueryable.Select(deduction => new ViewDeductionsResponse()
            {
                Id = deduction.Id,
                User = new User()
                {
                    Id = deduction.User.Id,
                    ImageUrl = deduction.User.ImageUrl,
                    Name = $"{deduction.User.FirstName} {deduction.User.LastName}",
                    Email = deduction.User.Email,
                },
                FinanceName = $"{deduction.Finance.FirstName} {deduction.Finance.LastName}",
                DeductionRate = deduction.DeductionRate,
                DeductionDate = deduction.DeductionDate.ToString("yyyy-MM-dd hh:mm tt") ?? "N/A"
            }).ToPaginatedListAsync(pageNumber, 5);
            if (deduction.Data.Count().Equals(0))
                return ("DeductionsNotFound", null);

            foreach (var user in deduction.Data)
            {
                var roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.User.Id.ToString()));
                user.User.Role = roles.FirstOrDefault() ?? "NoRole";
            }
            return ("DeductionsFound", deduction);
        }

        public async Task<(string, Dictionary<int, double>?)> GetTotalDeductionsForEachUser(int year, int month)
        {
            var deductions = await GetTableNoTracking().Where(
                deduction => deduction.DeductionDate.Year.Equals(year) && deduction.DeductionDate.Month.Equals(month)
                ).GroupBy(deduction => deduction.UserId).Select(group => new TotalDeductionsResponse()
                {
                    UserId = group.Key,
                    TotalDeductions = group.Sum(deduction => Convert.ToDouble(deduction.DeductionRate))
                }).ToDictionaryAsync(d => d.UserId, d => d.TotalDeductions);
            if(deductions is null)
                return ("DeductionsNotFound", null);
            return ("DeductionsFound", deductions);
        }
    }
}
