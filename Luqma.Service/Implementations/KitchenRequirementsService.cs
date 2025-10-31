using Luqma.Data.DTOs.RequirementItems;
using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.KitchenRequirements;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Service.Implementations
{
    internal class KitchenRequirementsService : IKitchenRequirementsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;
        private readonly LuqmaDbContext context;

        public KitchenRequirementsService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager, LuqmaDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<string> ChangeKitchenRequirementsAsync(int id, string status)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";

            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var kitchenRequirments = await unitOfWork.KitchenRequirementsRepository.GetByIdAsync(id);
            if (kitchenRequirments is null)
                return "KitchenRequirementsNotFound";

            if (kitchenRequirments.Status.ToLower().Equals("accepted"))
                return "YouCanNotChangeStatusForThisKitchenRequirementsAlreadyAccepted";
            if (kitchenRequirments.Status.ToLower().Equals("rejected"))
                return "YouCanNotChangeStatusForThisKitchenRequirementsAlreadyRejected";
            kitchenRequirments.Status = status;
            var result = await unitOfWork.KitchenRequirementsRepository.UpdateAsync(kitchenRequirments);
            return result <= 0 ? "AnErrorOccurredWhileEditingTheStatus" : "TheStatusHasBeenModifiedSuccessfully";
        }

        public async Task<string> DeleteKitchenRequirementsAsync(int id)
        {
            var financeId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(financeId))
                return "FinanceEmployeeNotFound";

            var finance = await userManager.FindByIdAsync(financeId);
            if (finance is null)
                return "FinanceEmployeeNotFound";

            var kitchenRequirments = await unitOfWork.KitchenRequirementsRepository.GetByIdAsync(id);
            if (kitchenRequirments is null)
                return "KitchenRequirementsNotFound";

            var result = await unitOfWork.KitchenRequirementsRepository.DeleteAsync(kitchenRequirments);
            return result <= 0 ? "AnErrorOccurredWhileDeletingKitchenRequirements" : "KitchenRequirementsDeletedSuccessfully";
        }

        public async Task<(string, PaginatedResult<GetKitchenRequirementsResponse>?)> GetKitchenRequirementsAsync(int pageNumber)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(userId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var kitchenRequirements = unitOfWork.KitchenRequirementsRepository.GetTableNoTracking().AsQueryable();
            if (kitchenRequirements is null)
                return ("KitchenRequirementsNotFound", null);

            var requirements = await kitchenRequirements.Select(req => new GetKitchenRequirementsResponse()
            {
                Id = req.Id,
                ChefName = $"{req.Chef.FirstName} {req.Chef.LastName}",
                TotalPrice = req.TotalPrice,
                Status = req.Status,
                Note = req.Note,
                Date = req.Date.ToString("yyyy-MM-dd hh:mm tt")
            }).ToPaginatedListAsync(pageNumber, 5);
            if (requirements.Data.Count().Equals(0))
                return ("KitchenRequirementsNotFound", null);
            return ("KitchenRequirementsFound", requirements);
        }

        public async Task<(string, GetKitchenRequirementsResponse?)> GetKitchenRequirementsByIdAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(userId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var kitchenRequirements = await unitOfWork.KitchenRequirementsRepository.GetByIdAsync(id);
            if (kitchenRequirements is null)
                return ("KitchenRequirementsNotFound", null);
            return ("KitchenRequirementsFound", new GetKitchenRequirementsResponse()
            {
                Id = kitchenRequirements.Id,
                ChefName = $"{kitchenRequirements.Chef.FirstName} {kitchenRequirements.Chef.LastName}",
                TotalPrice = kitchenRequirements.TotalPrice,
                Status = kitchenRequirements.Status,
                Note = kitchenRequirements.Note,
                Date = kitchenRequirements.Date.ToString("yyyy-MM-dd hh:mm tt")
            });
        }

        public async Task<(string, GetKitchenRequirementsInfoResponse?)> GetKitchenRequirementsInfoAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("FinanceOrManagerNotFound", null);
            var financeOrManager = await userManager.FindByIdAsync(userId);
            if (financeOrManager is null)
                return ("FinanceOrManagerNotFound", null);

            var kitchenRequirements = await unitOfWork.KitchenRequirementsRepository.GetByIdAsync(id);
            if (kitchenRequirements is null)
                return ("KitchenRequirementsNotFound", null);

            var requirmentItems = unitOfWork.RequirementItemsRepository.GetTableNoTracking()
                .Where(item => item.RequirmentId.Equals(id)).ToList();

            var items = requirmentItems.Select(item => new Items()
            {
                ItemId = item.KitchenItems.Id,
                Item = item.KitchenItems.Item,
                ImageUrl = item.KitchenItems.ImageUrl,
                Unit = item.KitchenItems.Unit,
                RequirementInfo = new RequirementInfo()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Discount = item.Discount
                }
            }).ToList();
            if (items is null || items.Count().Equals(0))
                return ("RequirementItemsNotFound", null);
            return ("KitchenRequirementsFound", new GetKitchenRequirementsInfoResponse
            {
                Id = kitchenRequirements.Id,
                ChefName = $"{kitchenRequirements.Chef.FirstName} {kitchenRequirements.Chef.LastName}",
                TotalPrice = kitchenRequirements.TotalPrice,
                Status = kitchenRequirements.Status,
                Note = kitchenRequirements.Note,
                Date = kitchenRequirements.Date.ToString("yyyy-MM-dd hh:mm tt"),
                Items = items,
            });
        }

        public async Task<(string, GetKitchenRequirementsResponse?)> PlaceNewKitchenRequirementsAsync(string? note, IList<RequirementItemsDTO> requirmentItems)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return ("ChefNotFound", null);
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return ("ChefNotFound", null);

            if (requirmentItems is null || requirmentItems.Count().Equals(0))
                return ("RequirementItemsNotFound", null);
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var kitchenRequirments = new KitchenRequirements()
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
                            return ("SomeKitchenItemNotFound", null);
                        }
                        var itemsPrice = (kitchenItem.Price * item.Quantity);
                        totalPrice += itemsPrice;
                        var newItem = new RequirementItems()
                        {
                            ItemId = item.ItemId,
                            Quantity = item.Quantity,
                            Price = itemsPrice,
                            Note = item.Note,
                        };
                        kitchenRequirments.RequirementItems.Add(newItem);
                    }
                    kitchenRequirments.TotalPrice = totalPrice;
                    var result = await unitOfWork.KitchenRequirementsRepository.AddAsync(kitchenRequirments);
                    if(result is null)
                    {
                        await transaction.RollbackAsync();
                        return ("AnErrorOccurredWhileAddingKitchenRequirement", null);
                    }
                    await transaction.CommitAsync();
                    return ("KitchenRequirementAddedSuccessfully", new GetKitchenRequirementsResponse()
                    {
                        Id = result.Id,
                        ChefName = $"{result.Chef.FirstName} {result.Chef.LastName}",
                        TotalPrice = result.TotalPrice,
                        Status = result.Status,
                        Note = result.Note,
                        Date = result.Date.ToString("yyyy-MM-dd hh:mm tt"),
                    });
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return ("AnErrorOccurredWhileAddingKitchenRequirement", null);
                }
            }
        }
    }
}
