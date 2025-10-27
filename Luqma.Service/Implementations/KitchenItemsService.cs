using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.KitchenItems;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Service.Implementations
{
    public class KitchenItemsService : IKitchenItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<LuqmaUser> userManager;
        private readonly LuqmaDbContext context;

        public KitchenItemsService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, UserManager<LuqmaUser> userManager,
            LuqmaDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<(string, GetKitchenItemsResponse?)> AddKitchenItemAsync(
            string item, string status, IFormFile? image, string? note, string unit, double quantity)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return ("ChefNotFound", null);
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return ("ChefNotFound", null);
            string imageUrl = "";
            if (image is not null)
            {
                try
                {
                    var guidPart = Guid.NewGuid().ToString("N").Substring(0, 12);
                    var datePart = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var id = $"{guidPart}-{datePart}";
                    using (var stream = image.OpenReadStream())
                    {
                        var (imageName, folderName) = (id, $"Luqma/Kitchen/Items/{item}/img");
                        var url = await cloudinaryService.UploadFileAsync(stream, folderName, imageName);
                        imageUrl = url;
                    }
                }
                catch (Exception exp)
                {
                    return ("AnErrorOccurredWhileProcessingItemImageUploadingRequest", null);
                }
            }
            var result = await unitOfWork.KitchenItemsRepository.AddAsync(new KitchenItems()
            {
                Item = item,
                Status = status,
                ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl,
                Note = string.IsNullOrWhiteSpace(note) ? null : note,
                Unit = unit,
                Quantity = quantity
            });
            if (result is null)
                return ("AnErrorOccurredWhileAddingKitchenItem", null);
            return ("KitchenItemAddedSuccessfully", new GetKitchenItemsResponse()
            {
                Id = result.Id,
                Item = result.Item,
                Status = result.Status,
                ImageUrl = result.ImageUrl,
                Note = result.Note,
                Unit = result.Unit,
                Quantity = result.Quantity,
            });
        }

        public async Task<string> DeleteKitchenItemAsync(int id)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return "ChefNotFound";
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return "ChefNotFound";

            var item = await unitOfWork.KitchenItemsRepository.GetByIdAsync(id);
            if (item is null)
                return "KitchenItemNotFound";
            var result = await unitOfWork.KitchenItemsRepository.DeleteAsync(item);
            return result <= 0 ? "AnErrorOccurredWhileDeletingKitchenItem" : "KitchenItemDeletedSuccessfully";
        }

        public async Task<(string, GetKitchenItemsResponse?)> GetKitchenItemByIdAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("ChefOrManagerNotFound", null);
            var chefOrManager = await userManager.FindByIdAsync(userId);
            if (chefOrManager is null)
                return ("ChefOrManagerNotFound", null);
            var item = await unitOfWork.KitchenItemsRepository.GetByIdAsync(id);
            if (item is null)
                return ("KitchenItemNotFound", null);
            return ("KitchenItemFound", new GetKitchenItemsResponse()
            {
                Id = item.Id,
                Item = item.Item,
                Status = item.Status,
                ImageUrl = item.ImageUrl,
                Note = item.Note,
                Unit = item.Unit,
                Quantity = item.Quantity,
            });
        }

        public async Task<(string, PaginatedResult<GetKitchenItemsResponse>?)> GetPaginatedKitchenItemsAsync(int pageNumber, string? search)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("ChefOrManagerNotFound", null);
            var chefOrManager = await userManager.FindByIdAsync(userId);
            if (chefOrManager is null)
                return ("ChefOrManagerNotFound", null);
            var items = unitOfWork.KitchenItemsRepository.GetTableNoTracking().AsQueryable();
            if (items is null)
                return ("KitchenItemsNotFound", null);
            if (!string.IsNullOrWhiteSpace(search))
                items = items.Where(item => item.Item.ToLower().Contains(search.ToLower()));
            var kitchenItems = await items.Select(item => new GetKitchenItemsResponse()
            {
                Id = item.Id,
                Item = item.Item,
                Status = item.Status,
                ImageUrl = item.ImageUrl,
                Note = item.Note,
                Unit = item.Unit,
                Quantity = item.Quantity,
            }).ToPaginatedListAsync(pageNumber, 5);
            if (kitchenItems.Data.Count().Equals(0))
                return ("KitchenItemsNotFound", null);
            return ("KitchenItemsFound", kitchenItems);
        }

        public async Task<string> UpdateItemStatusAsync(int id, string status)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return "ChefNotFound";
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return "ChefNotFound";

            var item = await unitOfWork.KitchenItemsRepository.GetByIdAsync(id);
            if (item is null)
                return "KitchenItemNotFound";

            item.Status = status;
            var result = await unitOfWork.KitchenItemsRepository.UpdateAsync(item);
            return result <= 0 ? "AnErrorOccurredWhileUpdatingKitchenItemStatus" : "KitchenItemStatusUpdatingSuccessfully";
        }

        public async Task<(string, GetKitchenItemsResponse?)> UpdateKitchenItemAsync(int id, string item, string status, string? note, string unit, double quantity)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return ("ChefNotFound", null);
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return ("ChefNotFound", null);

            var kitchenItem = await unitOfWork.KitchenItemsRepository.GetByIdAsync(id);
            if (kitchenItem is null)
                return ("KitchenItemNotFound", null);

            kitchenItem.Item = item;
            kitchenItem.Status = status;
            kitchenItem.Note = note;
            kitchenItem.Unit = unit;
            kitchenItem.Quantity = quantity;
            var result = await unitOfWork.KitchenItemsRepository.UpdateAsync(kitchenItem);
            return result <= 0 ? ("AnErrorOccurredWhileUpdatingKitchenItem", null)
                : ("KitchenItemUpdatingSuccessfully", new GetKitchenItemsResponse()
                {
                    Id = kitchenItem.Id,
                    Item = kitchenItem.Item,
                    Status = kitchenItem.Status,
                    ImageUrl = kitchenItem.ImageUrl,
                    Note = kitchenItem.Note,
                    Unit = kitchenItem.Unit,
                    Quantity = kitchenItem.Quantity,
                });
        }

        public async Task<(string, string?)> UploadItemImageAsync(int id, IFormFile image)
        {
            var chefId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(chefId))
                return ("ChefNotFound", null);
            var chef = await userManager.FindByIdAsync(chefId);
            if (chef is null)
                return ("ChefNotFound", null);

            var item = await unitOfWork.KitchenItemsRepository.GetByIdAsync(id);
            if (item is null)
                return ("KitchenItemNotFound", null);

            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var originalUrl = item.ImageUrl;
                if (!string.IsNullOrWhiteSpace(originalUrl))
                {
                    var cloudinaryResult = await cloudinaryService.DeleteFileAsync(originalUrl);
                    if (cloudinaryResult.Equals("FailedToDeleteImageFromCloudinary"))
                        return ("FailedToDeleteImageFromCloudinary", null);
                }
                var guidPart = Guid.NewGuid().ToString("N").Substring(0, 12);
                var datePart = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var fullPart = $"{guidPart}-{datePart}";
                using (var stream = image.OpenReadStream())
                {
                    var (imageName, folderName) = (fullPart, $"Luqma/Kitchen/Items/{item.Item}/img");
                    var url = await cloudinaryService.UploadFileAsync(stream, folderName, imageName);
                    item.ImageUrl = url;
                }
                var result = await unitOfWork.KitchenItemsRepository.UpdateAsync(item);
                if(result <= 0)
                {
                    await transaction.RollbackAsync();
                    return ("AnErrorOccurredWhileProcessingImageModificationRequest", null);
                }
                await transaction.CommitAsync();
                return ("TheImageHasBeenChangedSuccessfully", item.ImageUrl);
            }
            catch (Exception exp)
            {
                if (transaction.GetDbTransaction().Connection is not null)
                    await transaction.RollbackAsync();
                return ("AnErrorOccurredWhileProcessingImageModificationRequest", null);
            }
        }
    }
}
