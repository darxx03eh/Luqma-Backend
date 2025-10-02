using Luqma.Data.Response.UsersManagements;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface IUsersManagementService
    {
        public Task<(string, PaginatedResult<ViewUsersResponse>?)> ViewUsersAsync(int pageNumber, int pageSize);
        public Task<string> DeActivateAsync(int id);
        public Task<string> ActivateAsync(int id);
    }
}
