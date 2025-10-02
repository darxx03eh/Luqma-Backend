using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.UsersManagements;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Service.Implementations
{
    public class UsersManagementService : IUsersManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;

        public UsersManagementService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
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
    }
}
