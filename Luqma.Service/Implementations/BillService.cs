using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Bills;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Service.Implementations
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;

        public BillService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<(string, BillsResponse?)> AddNewBillAsync(string billType, double totalPrice, string note)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return ("FinanceEmployeeNotFound", null);
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return ("FinanceEmployeeNotFound", null);

            var result = await unitOfWork.BillRepository.AddAsync(new Bill()
            {
                BillType = billType,
                Note = note ?? null,
                TotalPrice = totalPrice,
                FinanceId = finance.Id,
                Status = "Pending",
            });
            if (result is null)
                return ("AnErrorOccurredWhileAddingTheBill", null);
            var bill = new BillsResponse()
            {
                Id = result.Id,
                FinanceName = $"{finance.FirstName} {finance.LastName}",
                BillType = result.BillType,
                Amount = result.TotalPrice,
                DueDate = result.DueDate is null ? "N/A" : result.DueDate.Value.ToString("yyyy-MM-dd hh:mm tt"),
                Status = result.Status,
            };
            return ("TheBillHasBeenAddedSuccessfully", bill);
        }

        public async Task<string> DeleteBillAsync(int id)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var bill = await unitOfWork.BillRepository.GetByIdAsync(id);
            if (bill is null)
                return "BillNotFound";

            var result = await unitOfWork.BillRepository.DeleteAsync(bill);
            return result <= 0 ? "AnErrorOccurredWhileDeletingTheBill" : "TheBillHasBeenDeletedSuccessfully";
        }

        public async Task<(string, PaginatedResult<BillsResponse>?)> GetBillsAsync(int pageNumber)
        {
            var (result, bills) = await unitOfWork.BillRepository.GetBillsAsync(pageNumber);
            return result switch
            {
                "BillsNotFound" => ("BillsNotFound", null),
                "BillsFound" => ("BillsFound", bills),
                _ => ("BillsNotFound", null)
            };
        }

        public async Task<string> UpdateBillAsync(int id, string billType, string note, double totalPrice)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var bill = await unitOfWork.BillRepository.GetByIdAsync(id);
            if (bill is null)
                return "BillNotFound";

            bill.BillType = billType;
            bill.Note = note;
            bill.TotalPrice = totalPrice;
            var result = await unitOfWork.BillRepository.UpdateAsync(bill);
            return result <= 0 ? "AnErrorOccurredWhileEditingTheBill" : "TheBillHasBeenModifiedSuccessfully";
        }

        public async Task<string> UpdateBillStatusAsync(int id, string status)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";
            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var bill = await unitOfWork.BillRepository.GetByIdAsync(id);
            if (bill is null)
                return "BillNotFound";

            bill.Status = status;
            if (status.ToLower().Equals("paid"))
                bill.DueDate = DateTime.UtcNow;

            var result = await unitOfWork.BillRepository.UpdateAsync(bill);
            return result <= 0 ? "AnErrorOccurredWhileEditingTheStatus" : "TheStatusHasBeenModifiedSuccessfully";
        }
    }
}
