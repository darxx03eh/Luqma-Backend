using Luqma.Data.DTOs.RequirmentItems;
using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Service.Implementations
{
    internal class KitchenRequirmentsService : IKitchenRequirmentsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;
        private readonly LuqmaDbContext context;

        public KitchenRequirmentsService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager, LuqmaDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<string> ChangeKitchenRequirmentsAsync(int id, string status)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";

            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var kitchenRequirments = await unitOfWork.KitchenRequirmentsRepository.GetByIdAsync(id);
            if (kitchenRequirments is null)
                return "KitchenRequirmentsNotFound";

            if (kitchenRequirments.Status.ToLower().Equals("accepted"))
                return "YouCanNotChangeStatusForThisKitchenRequirmentsAlreadyAccepted";
            if (kitchenRequirments.Status.ToLower().Equals("rejected"))
                return "YouCanNotChangeStatusForThisKitchenRequirmentsAlreadyRejected";
            kitchenRequirments.Status = status;
            var result = await unitOfWork.KitchenRequirmentsRepository.UpdateAsync(kitchenRequirments);
            return result <= 0 ? "AnErrorOccurredWhileEditingTheStatus" : "TheStatusHasBeenModifiedSuccessfully";
        }

        public async Task<string> PlaceNewKitchenRequirmentsAsync(string? note, IList<RequirmentItemsDTO> requirmentItems)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return "ChefNotFound";
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return "ChefNotFound";

            if (requirmentItems is null || requirmentItems.Count().Equals(0))
                return "RequirmentItemsNotFound";
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var kitchenRequirments = new KitchenRequirments()
                    {
                        ChefId = Convert.ToInt32(chefId),
                        Note = note ?? null,
                        Status = "Pending",
                        Date = DateTime.UtcNow
                    };
                    double totalPrice = 0;
                    foreach(var item in requirmentItems)
                    {
                        var kitchenItem = await unitOfWork.KitchenItemsRepository.GetByIdAsync(item.ItemId);
                        if(kitchenItem is null)
                        {
                            await transaction.RollbackAsync();
                            return "SomeKitchenItemNotFound";
                        }
                        var itemsPrice = (kitchenItem.Price * item.Quantity) - item.Discount;
                        totalPrice += itemsPrice;
                        var newItem = new RequirmentItems()
                        {
                            ItemId = item.ItemId,
                            Quantity = item.Quantity,
                            Discount = item.Discount,
                            Price = itemsPrice,
                        };
                        kitchenRequirments.RequirmentItems.Add(newItem);
                    }
                    kitchenRequirments.TotalPrice = totalPrice;
                    var result = await unitOfWork.KitchenRequirmentsRepository.AddAsync(kitchenRequirments);
                    if(result is null)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileAddingKitchenRequirment";
                    }
                    await transaction.CommitAsync();
                    return "KitchenRequirmentAddedSuccessfully";
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return "AnErrorOccurredWhileAddingKitchenRequirment";
                }
            }
        }
    }
}
