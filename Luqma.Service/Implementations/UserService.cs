using Luqma.Data.DTOs.Users;
using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;
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

        public async Task<string> ActivateAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return "TheUserWhoseAccountYouWantToActivateIsNotFound";
            if (user.IsActive)
                return "UserAlreadyActive";
            user.IsActive = true;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return "AnErrorOccurredWhileActivatingTheUser";
            return "TheUserHasBeenActivatedSuccessfully";
        }

        public async Task<string> DeActivateAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return "UserNotFound";
            user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return "TheUserWhoseAccountYouWantToDeactivateIsNotFound";
            if (!user.IsActive)
                return "UserAlreadyInActive";
            user.IsActive = false;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return "AnErrorOccurredWhileDeactivatingTheUser";
            return "TheUserHasBeenDeactivatedSuccessfully";
        }

        public async Task<(string, PaginatedResult<ViewUsersResponse>?)> ViewUsersAsync(int pageNumber, int pageSize)
        {
            var (result, users) = await unitOfWork.UserRepository.ViewUsersAsync(pageNumber, pageSize);
            return result switch
            {
                "UsersNotFound" => ("UsersNotFound", null),
                "UsersFound" => ("UsersFound", users),
                _ => ("UsersNotFound", null)
            };
        }

        public async Task<string> AddAddressAsync(string city, string state, string street)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var result = await unitOfWork.UserAddressRepository.AddAsync(new UserAddress()
            {
                UserId = int.Parse(userId),
                City = city,
                State = state,
                Street = street
            });
            return result is null ? "AnErrorOccurredWhileAddingTheAddress" : "TheAddressHasBeenAddedSuccessfully";
        }

        public async Task<string> UpdateAddressAsync(string city, string state, string street, int addressId)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var address = await unitOfWork.UserAddressRepository.GetByIdAsync(addressId);
            if (address is null)
                return "AddressNotFound";
            var userOwnAddress = await userManager.FindByIdAsync(address.UserId.ToString());
            if (!userId.Equals(userOwnAddress.Id.ToString()))
                return "ThisAddressDoesNotBelongToYou";
            address.City = city;
            address.State = state;
            address.Street = street;
            var result = await unitOfWork.UserAddressRepository.UpdateAsync(address);
            return result <= 0 ? "AnErrorOccurredWhileEditingTheAddress." : "TheAddressHasBeenSuccessfullyModified";
        }

        public async Task<string> DeleteAddressAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return "UserNotFound";
            var address = await unitOfWork.UserAddressRepository.GetByIdAsync(id);
            if (address is null)
                return "AddressNotFound";
            var userOwnAddress = await userManager.FindByIdAsync(address.UserId.ToString());
            if (!userId.Equals(userOwnAddress.Id.ToString()))
                return "ThisAddressDoesNotBelongToYou";
            var result = await unitOfWork.UserAddressRepository.DeleteAsync(address);
            return result <= 0 ? "AnErrorOccurredWhileDeletingTheAddress" : "TheAddressHasBeenSuccessfullyDeleted";
        }

        public async Task<(string, PaginatedResult<ShowUserAddressResponse>?)> ShowUserAddressesAsync(int pageNumber)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("UserNotFound", null);
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return ("UserNotFound", null);
            var (result, addresses) = await unitOfWork.UserAddressRepository.GetUserAddressesAsync(int.Parse(userId), pageNumber, 5);
            return result switch
            {
                "AddressesNotFound" => ("AddressesNotFound", null),
                "AddressesFound" => ("AddressesFound", addresses),
                _ => ("AddressesNotFound", null)
            };
        }

        public async Task<(string, ShowUserAddressResponse?)> ViewSpecificAddressAsync(int id)
        {
            var userId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
                return ("UserNotFound", null);
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return ("UserNotFound", null);
            var address = await unitOfWork.UserAddressRepository.GetByIdAsync(id);
            if (address is null)
                return ("AddressNotFound", null);
            //var userOwnAddress = await userManager.FindByIdAsync(address.UserId.ToString());
            //if (!userId.Equals(userOwnAddress.Id.ToString()))
            //    return ("ThisAddressDoesNotBelongToYou", null);
            return ("AddressFound", new ShowUserAddressResponse()
            {
                Id = address.Id,
                City = address.City,
                State = address.State,
                Street = address.Street
            });
        }

        public async Task<(string, ProfileResponse?)> GetUserProfileAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user is null)
                return ("UserNotFound", null);
            try
            {
                var profile = new ProfileResponse()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    Role = string.Join(", ", await userManager.GetRolesAsync(user)),
                    IsActive = user.IsActive,
                    Gender = user.Gender.ToString(),
                    BirthDate = user.BirthDate.ToString("MMMM dd, yyyy"),
                    JoinDate = user.CreatedAt.ToString("MMMM dd, yyyy")
                };
                return ("UserFound", profile);
            }
            catch (Exception exp)
            {
                return ("ThereWasAProblemLoadingTheProfile", null);
            }
        }

        public async Task<string> ChangeUserRolesAsync(int userId, IList<UserRolesDTO> roles)
        {
            var managerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(managerId))
                return "ManagerNotFound";

            var user = await userManager.FindByIdAsync(Convert.ToString(userId));
            if (user is null)
                return "UserNotFound";
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var oldRoles = await userManager.GetRolesAsync(user);
                    var deletedResult = await userManager.RemoveFromRolesAsync(user, oldRoles);
                    if (!deletedResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileDeletingOldRoles";
                    }
                    var selectedRoles = roles.Where(role => role.HasRole).Select(role => role.RoleName);
                    var roleResult = await userManager.AddToRolesAsync(user, selectedRoles);
                    if (!roleResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "FailedToAddUserRoles";
                    }
                    await transaction.CommitAsync();
                    return "AddedToUserRolesSuccessfully";
                }
                catch (Exception exp)
                {
                    if (transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return "AnErrorOccurredWhileAddingTheUserToRoles";
                }
            }
        }

        public async Task<string> UpdateUserDataAsync(UpdateUserDataDTO request)
        {
            var managerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(managerId))
                return "ManagerNotFound";

            var user = await userManager.FindByIdAsync(Convert.ToString(request.UserId));
            if (user is null)
                return "UserNotFound";

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.Gender = request.Gender;
            user.BirthDate = request.BirthDate;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return "AnErrorOccurredWhileUpdatingUserData";
            return "UserDataUpdatedSuccessfully";
        }

        public async Task<string> ChangePasswordForUserByManagerAsync(int userId, string password)
        {
            var managerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(managerId))
                return "ManagerNotFound";
            var user = await userManager.FindByIdAsync(Convert.ToString(userId));
            if (user is null)
                return "UserNotFound";
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var deleteResult = await userManager.RemovePasswordAsync(user);
                    if (!deleteResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return "AnErrorOccurredWhileDeletingTheOldPassword";
                    }
                    var addResult = await userManager.AddPasswordAsync(user, password);
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

        public async Task<string> ChangeSalaryAsync(int userId, double salary)
        {
            var managerId = unitOfWork.UserRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(managerId))
                return "ManagerNotFound";
            var user = await userManager.FindByIdAsync(Convert.ToString(userId));
            if (user is null)
                return "UserNotFound";

            user.Salary = Convert.ToDecimal(salary);
            var result = await userManager.UpdateAsync(user);
            return !result.Succeeded ? "AnErrorOccurredWhileUpdatingSalary" : "SalaryForSpecificUserUpdatedSuccessfully";
        }
    }
}