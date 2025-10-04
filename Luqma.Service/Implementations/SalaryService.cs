using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Service.Implementations
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;

        public SalaryService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<string> GenerateSalaryAsync()
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";
            var date = DateTime.UtcNow;
            var (deductionResult, deductions) = await unitOfWork.DeductionRepository.GetTotalDeductionsForEachUser(date.Year, date.Month);
            var salariesDate = await unitOfWork.SalaryRepository.GetTableNoTracking()
                               .Where(salary => salary.SalaryDate.Year.Equals(date.Year) && salary.SalaryDate.Month.Equals(date.Month))
                               .FirstOrDefaultAsync();
            if (salariesDate is not null)
                return "SalariesForThisYearAndMonthAlreadyGenerated";
            IList<Salary> salaries = new List<Salary>();
            var users = await userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                var salary = (double)user.Salary;
                if (deductions.TryGetValue(user.Id, out var deduction))
                    salary = salary - (salary * (deduction / 100));

                var UserSalary = new Salary()
                {
                    UserId = user.Id,
                    FinanceId = int.Parse(financeId),
                    Status = "Pending",
                    SalaryDate = DateTime.UtcNow,
                    SalaryAmount = salary
                };
                salaries.Add(UserSalary);
            }
            try
            {
                await unitOfWork.SalaryRepository.AddRangeAsync(salaries);
                return "SalariesGeneratedSuccessfully";
            }
            catch(Exception exp)
            {
                return "AnErrorOccurredWhileGeneratingSalaries";
            }
        }
    }
}
