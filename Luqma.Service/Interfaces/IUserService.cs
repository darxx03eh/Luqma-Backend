using Luqma.Data.DTOs.Users;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Luqma.Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CheckPasswordAsync(string password);
        public Task<bool> CheckUserNameAsync(string username);
        public Task<bool> IsUserExistAsync(int id);
        public Task<string> ChangePasswordAsync(string password, string newPasswordConfirm);
        public Task<string> ChangeNameAsync(string firstName, string lastName);
        public Task<(string, string?)> ChangeUserNameAsync(string username);
        public Task<(string, string?)> UploadProfileImageAsync(IFormFile image);
        public Task<string> DeleteProfileImageAsync();
        public Task<string> ChangeBirthDateAsync(DateTime birthDate);
        public Task<(string, PaginatedResult<ViewUsersResponse>?)> ViewUsersAsync(int pageNumber, int pageSize);
        public Task<string> DeActivateAsync(int id);
        public Task<string> ActivateAsync(int id);
        public Task<string> AddAddressAsync(string city, string state, string street);
        public Task<string> UpdateAddressAsync(string city, string state, string street, int addressId);
        public Task<string> DeleteAddressAsync(int id);
        public Task<(string, PaginatedResult<ShowUserAddressResponse>?)> ShowUserAddressesAsync(int pageNumber);
        public Task<(string, ShowUserAddressResponse?)> ViewSpecificAddressAsync(int id);
        public Task<(string, ProfileResponse?)> GetUserProfileAsync(string username);
        public Task<string> ChangeUserRolesAsync(int userId, IList<UserRolesDTO> roles);
        public Task<string> UpdateUserDataAsync(UpdateUserDataDTO request);
        public Task<string> ChangePasswordForUserByManagerAsync(int userId, string password);
    }
}
