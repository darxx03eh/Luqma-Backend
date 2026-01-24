using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Deductions;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Service.Implementations
{
    public class DeductionService : IDeductionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;

        public DeductionService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<string> AddDeductionToUserAsync(int userId, double deductionRate)
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
            var result = await unitOfWork.DeductionRepository.AddAsync(new Deduction()
            {
                UserId = userId,
                FinanceId = finance.Id,
                DeductionRate = deductionRate,
                DeductionDate = DateTime.UtcNow
            });
            return result is null ? "AnErrorOccurredWhileAddingTheDeduction" : "TheDeductionHasBeenAddedSuccessfully";
        }

        public async Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsByDateAsync(int pageNumber, int year, int month)
        {
            var financeOrManagerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeOrManagerId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(financeOrManagerId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var (result, deductions) = await unitOfWork.DeductionRepository.GetAllDeductionsByDateAsync(pageNumber, year, month);
            return result switch
            {
                "DeductionsNotFound" => ("DeductionsNotFound", null),
                "DeductionsFound" => ("DeductionsFound", deductions),
                _ => ("DeductionsNotFound", null)
            };
        }

        public async Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsForSpecificUser(int pageNumber, string name)
        {
            var financeOrManagerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeOrManagerId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(financeOrManagerId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var (result, deductions) = await unitOfWork.DeductionRepository.GetAllDeductionsForSpecificUser(pageNumber, name);
            return result switch
            {
                "DeductionsNotFound" => ("DeductionsNotFound", null),
                "DeductionsFound" => ("DeductionsFound", deductions),
                _ => ("DeductionsNotFound", null)
            };
        }

        public async Task<string> RemoveDeductionFromUserAsync(int id)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";
            var deduction = await unitOfWork.DeductionRepository.GetByIdAsync(id);
            if (deduction is null)
                return "DeductionNotFound";
            var result = await unitOfWork.DeductionRepository.DeleteAsync(deduction);
            return result <= 0 ? "AnErrorOccurredWhileDeletingTheDeduction" : "TheDeductionHasBeenSuccessfullyRemoved";
        }

        public async Task<(string, PaginatedResult<ViewDeductionsResponse>?)> ShowAllDeductionsAsync(int pageNumber)
        {
            var financeOrManagerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeOrManagerId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(financeOrManagerId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var deductionsQueryable = unitOfWork.DeductionRepository.GetTableNoTracking()
                                      .OrderByDescending(deduction => deduction.DeductionDate)
                                      .AsQueryable();
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

        public async Task<string> UpdateDeductionAsync(int deductionId, double deductionRate)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";
            var deduction = await unitOfWork.DeductionRepository.GetByIdAsync(deductionId);
            if (deduction is null)
                return "DeductionNotFound";
            deduction.DeductionRate = deductionRate;
            deduction.DeductionDate = DateTime.UtcNow;
            var result = await unitOfWork.DeductionRepository.UpdateAsync(deduction);
            return result <= 0 ? "ModifyingDeductionFailed" : "TheDeductionModificationProcessWasCompletedSuccessfully";
        }
    }
}
