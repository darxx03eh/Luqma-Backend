using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Deductions;
using Luqma.Data.Response.Salaries;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Repositories
{
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<LuqmaUser> userManager;

        public SalaryRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<LuqmaUser> userManager)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<(string, PaginatedResult<GetSalariesResponse>?)> GetSalariesAsync(int pageNumber, string search, string filter,
                                                                                            int? year = 0, int? month = 0)
        {
            var salariesQueryable = GetTableNoTracking().OrderByDescending(salary => salary.SalaryAmount).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                salariesQueryable = salariesQueryable.Where(
                    salary => EF.Functions.Like((salary.User.FirstName + " " + salary.User.LastName), $"%{search}%")
                    );
            if (!string.IsNullOrWhiteSpace(filter))
                salariesQueryable = salariesQueryable.Where(salary => salary.Status.ToLower().Equals(filter.ToLower()));

            if (!year.Equals(0))
                salariesQueryable = salariesQueryable.Where(salary => salary.SalaryDate.Year.Equals(year));
            if (!month.Equals(0))
                salariesQueryable = salariesQueryable.Where(salary => salary.SalaryDate.Month.Equals(month));

            if (salariesQueryable is null)
                return ("SalariesNotFound", null);

            var salaries = await salariesQueryable.Select(salary => new GetSalariesResponse()
            {
                Id = salary.Id,
                User = new User()
                {
                    Id = salary.UserId,
                    ImageUrl = salary.User.ImageUrl,
                    Name = $"{salary.User.FirstName} {salary.User.LastName}",
                    Email = salary.User.Email,
                },
                FinanceName = $"{salary.Finance.FirstName} {salary.Finance.LastName}",
                Status = salary.Status,
                SalarayAdmount = salary.SalaryAmount,
            }).ToPaginatedListAsync(pageNumber, 5);

            if (salaries.Data.Count().Equals(0))
                return ("SalariesNotFound", null);
            foreach (var user in salaries.Data)
            {
                var roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.User.Id.ToString()));
                user.User.Role = roles.FirstOrDefault() ?? "NoRole";
            }
            return ("SalariesFound", salaries);
        }
    }
}
