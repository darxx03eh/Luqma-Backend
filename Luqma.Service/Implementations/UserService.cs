using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;
        private readonly LuqmaDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public UserService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager, LuqmaDbContext context
                          , ICloudinaryService cloudinaryService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> ChangeBirthDateAsync(DateTime birthDate)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            user.BirthDate = birthDate;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return "AnErrorOccurredWhileChangingTheBirthDate";
            return "BirthDateChangedSuccessfully";
        }

        public async Task<string> ChangeNameAsync(string firstName, string lastName)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    user.FirstName = firstName;
                    var first = await userManager.UpdateAsync(user);
                    if (!first.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileChangingTheFirstName";
                    }
                    user.LastName = lastName;
                    var last = await userManager.UpdateAsync(user);
                    if (!last.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileChangingTheLastName";
                    }
                    await transaction.CommitAsync();
                    return "NameChangedSuccessfully";
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return "AnErrorOccurredWhileChangingTheName";
                }
            }
        }

        public async Task<string> ChangePasswordAsync(string password, string newPassword)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var checkPassword = await userManager.CheckPasswordAsync(user, password);
                    if (!checkPassword)
                    {
                        await transaction.RollbackAsync();
                        return "CurrentPasswordWrong";
                    }
                    var deleteResult = await userManager.RemovePasswordAsync(user);
                    if (!deleteResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileDeletingTheOldPassword";
                    }
                    var addResult = await userManager.AddPasswordAsync(user, newPassword);
                    if (!addResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileAddingTheNewPassword";
                    }
                    await transaction.CommitAsync();
                    return "PasswordChangedSuccessfully";
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return "AnErrorOccurredWhileChangingThePassword";
                }
            }
        }

        public async Task<(string, string?)> ChangeUserNameAsync(string username)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("UserNotFound", null);

            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return ("UserNotFound", null);
            var result = await userManager.SetUserNameAsync(user, username);
            if (!result.Succeeded)
                return ("AnErrorOccurredWhileChangingTheUsername", null);
            return ("UsernameChangedSuccessfully", username);
        }

        public async Task<bool> CheckPasswordAsync(string password)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return false;
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> CheckUserNameAsync(string username)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return false;
            var currentUser = await userManager.FindByIdAsync(userId);
            if (currentUser == null)
                return false;
            if (currentUser.UserName.Equals(username))
                return false;
            var existingUser = await userManager.FindByNameAsync(username);
            if (existingUser != null && !existingUser.Id.Equals(userId))
                return false;
            return true;
        }

        public async Task<string> DeleteProfileImageAsync()
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            var url = user.ImageUrl;
            if (string.IsNullOrWhiteSpace(url))
                return "ThereIsNoImageToDelete";
            try
            {
                var cloudinaryResult = cloudinaryService.DeleteFileAsync(url);
                if (cloudinaryResult.Equals("FailedToDeleteImageFromCloudinary"))
                    return "FailedToDeleteImageFromCloudinary";
                user.ImageUrl = null;
                var result = await userManager.UpdateAsync(user);
                return result.Succeeded ? "ImageHasBeenSuccessfullyDeleted" : "AnErrorOccurredWhileSaving";
            }
            catch (Exception exp)
            {
                return "AnErrorOccurredWhileDeletingTheImage";
            }
        }

        public async Task<bool> IsUserExistAsync(int id)
            => await userManager.FindByIdAsync(id.ToString()) is not null;

        public async Task<(string, string?)> UploadProfileImageAsync(IFormFile image)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("UserNotFound", null);
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return ("UserNotFound", null);
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var originalUrl = user.ImageUrl;
                    if (string.IsNullOrWhiteSpace(originalUrl))
                    {
                        var cloudinaryResult = await cloudinaryService.DeleteFileAsync(originalUrl);
                        if (cloudinaryResult.Equals("FailedToDeleteImageFromCloudinary"))
                            return ("FailedToDeleteOldImageFromCloudinary", null);
                    }
                    var guidPart = Guid.NewGuid().ToString("N").Substring(0, 12);
                    var datePart = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var id = $"{guidPart}-{datePart}";
                    using (var stream = image.OpenReadStream())
                    {
                        var (imageName, folderName) = (id, $"Luqma/Accounts/{user.Id}/ProfileImage");
                        var url = await cloudinaryService.UploadFileAsync(stream, folderName, imageName);
                        user.ImageUrl = url;
                    }
                    var result = await userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return ("AnErrorOccurredWhileEditingImage", null);
                    }
                    await transaction.CommitAsync();
                    return ("TheImageHasBeenChangedSuccessfully", user.ImageUrl);
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return ("AnErrorOccurredWhileProcessingYourProfileImageModificationRequest", null);
                }
            }
        }
    }
}
