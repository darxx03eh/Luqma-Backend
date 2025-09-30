using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luqma.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<LuqmaUser> userManager;
        private readonly LuqmaDbContext context;

        public UserService(IUnitOfWork unitOfWork, UserManager<LuqmaUser> userManager, LuqmaDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.context = context;
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
                }catch(Exception exp)
                {
                    if(transaction.GetDbTransaction().Connection is not null)
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
                catch(Exception exp)
                {
                    if(transaction.GetDbTransaction().Connection is not null)
                        await transaction.RollbackAsync();
                    return "AnErrorOccurredWhileChangingThePassword";
                }
            }
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
    }
}
