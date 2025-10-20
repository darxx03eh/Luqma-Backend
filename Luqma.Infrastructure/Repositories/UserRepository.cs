using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<LuqmaUser>, IUserRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<LuqmaUser> userManager;

        public UserRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<LuqmaUser> userManager)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<(string, PaginatedResult<ViewUsersResponse>?)> ViewUsersAsync(int pageNumber, int pageSize)
        {
            var usersQueryable = GetTableNoTracking().OrderByDescending(user => user.LastLogin).AsQueryable();
            if (usersQueryable is null)
                return ("UsersNotFound", null);
            var users = await usersQueryable.Select(user => new ViewUsersResponse()
            {
                Id = user.Id,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Status = user.IsActive ? "Active" : "InActive",
                LastLogin = user.LastLogin.Value.ToString("yyyy-MM-dd hh:mm tt") ?? "N/A"
            }).ToPaginatedListAsync(pageNumber, pageSize);
            if (users.Data.Count().Equals(0))
                return ("UsersNotFound", null);
            foreach (var user in users.Data)
            {
                var roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id.ToString()));
                user.Role = roles.FirstOrDefault() ?? "NoRole";
            }
            return ("UsersFound", users);
        }
    }
}
