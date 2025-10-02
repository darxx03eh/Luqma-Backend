using Luqma.Core.Bases;
using Luqma.Core.Features.Users.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Handlers
{
    public class UsersManagementQueryHandler : ApiResponseHandler
        , IRequestHandler<ViewUsersCommand, ApiResponse>
    {
        private readonly IUserService userService;

        public UsersManagementQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ApiResponse> Handle(ViewUsersCommand request, CancellationToken cancellationToken)
        {
            var (result, users) = await userService.ViewUsersAsync(request.PageNumber, 5);
            return result switch
            {
                "UsersNotFound" => NotFound(SharedResponseKeys.UsersNotFound),
                "UsersFound" => Success(users, message: SharedResponseKeys.UsersFound),
                _ => NotFound(SharedResponseKeys.UsersNotFound)
            };
        }
    }
}
