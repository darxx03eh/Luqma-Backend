using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
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
