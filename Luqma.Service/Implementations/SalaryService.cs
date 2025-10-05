using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Salaries;
using Luqma.Data.Wrappers;
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

        public async Task<string> ChangeSalaryAmountAsync(int id, double amount)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var salary = await unitOfWork.SalaryRepository.GetByIdAsync(id);
            if (salary is null)
                return "SalaryNotFound";

            salary.SalaryAmount = amount;
            var result = await unitOfWork.SalaryRepository.UpdateAsync(salary);
            return result <= 0 ? "AnErrorOccurredWhileUpdatingTheSalary" : "AmountUpdatedSuccessfully";
        }

        public async Task<string> ChangeSalaryStatusAsync(int id, string status)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var salary = await unitOfWork.SalaryRepository.GetByIdAsync(id);
            if (salary is null)
                return "SalaryNotFound";

            salary.Status = status;
            var result = await unitOfWork.SalaryRepository.UpdateAsync(salary);
            return result <= 0 ? "AnErrorOccurredWhileUpdatingTheStatus" : "StatusUpdatedSuccessfully";
        }

        public async Task<string> DeleteSalaryAsync(int id)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var salary = await unitOfWork.SalaryRepository.GetByIdAsync(id);
            if (salary is null)
                return "SalaryNotFound";

            var result = await unitOfWork.SalaryRepository.DeleteAsync(salary);
            return result <= 0 ? "AnErrorOccurredWhileDeletingTheSalary" : "SalaryDeletedSuccessfully";
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
            foreach (var user in users)
            {
                var salary = (double)user.Salary;
                if (deductions.TryGetValue(user.Id, out var deduction))
                    salary = salary - (salary * (deduction / 100));

                var userSalary = new Salary()
                {
                    UserId = user.Id,
                    FinanceId = int.Parse(financeId),
                    Status = "Pending",
                    SalaryDate = DateTime.UtcNow,
                    SalaryAmount = salary
                };
                salaries.Add(userSalary);
            }
            try
            {
                await unitOfWork.SalaryRepository.AddRangeAsync(salaries);
                return "SalariesGeneratedSuccessfully";
            }
            catch (Exception exp)
            {
                return "AnErrorOccurredWhileGeneratingSalaries";
            }
        }

        public async Task<string> GenerateSalaryForUserAsync(int userId, int? year = 0, int? month = 0)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return "UserNotFound";
            var date = DateTime.UtcNow;
            if (year.Equals(0) && month.Equals(0))
                (year, month) = (date.Year, date.Month);

            var (deductionResult, deduction) = await unitOfWork.DeductionRepository.GetTotalDeductionsForUser(userId, year, month);
            var salariesDate = await unitOfWork.SalaryRepository.GetTableNoTracking()
                               .Where(salary => salary.SalaryDate.Year.Equals(date.Year) && salary.SalaryDate.Month.Equals(date.Month)
                               && salary.UserId.Equals(userId))
                               .FirstOrDefaultAsync();
            if (salariesDate is not null)
                return "SalaryForThisYearAndMonthAlreadyGeneratedForThisUser";

            var salary = (double)user.Salary;
            if (deduction.TryGetValue(userId, out var d))
                salary = salary - (salary * (d / 100));

            var userSalary = new Salary()
            {
                UserId = user.Id,
                FinanceId = int.Parse(financeId),
                Status = "Pending",
                SalaryDate = DateTime.UtcNow,
                SalaryAmount = salary
            };

            var result = await unitOfWork.SalaryRepository.AddAsync(userSalary);
            return result is null ? "AnErrorOccurredWhileGeneratingSalary" : "SalaryGeneratedSuccessfully";
        }

        public async Task<(string, PaginatedResult<GetSalariesResponse>?)> GetSalariesAsync(int pageNumber, string search, string filter,
                                                                                      int? year = 0, int? month = 0)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return ("FinanceEmployeeNotFound", null);
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return ("FinanceEmployeeNotFound", null);

            var (result, salaries) = await unitOfWork.SalaryRepository.GetSalariesAsync(pageNumber, search, filter, year, month);
            return result switch
            {
                "SalariesNotFound" => ("SalariesNotFound", null),
                "SalariesFound" => ("SalariesFound", salaries),
                _ => ("SalariesNotFound", null)
            };
        }
    }
}
