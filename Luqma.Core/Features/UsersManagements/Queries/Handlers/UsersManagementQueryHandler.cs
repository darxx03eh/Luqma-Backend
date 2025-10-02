using Luqma.Core.Bases;
using Luqma.Core.Features.UsersManagements.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.UsersManagements.Queries.Handlers
{
    public class UsersManagementQueryHandler : ApiResponseHandler
        , IRequestHandler<ViewUsersCommand, ApiResponse>
    {
        private readonly IUsersManagementService usersManagementService;

        public UsersManagementQueryHandler(IUsersManagementService usersManagementService)
        {
            this.usersManagementService = usersManagementService;
        }
        public async Task<ApiResponse> Handle(ViewUsersCommand request, CancellationToken cancellationToken)
        {
            var (result, users) = await usersManagementService.ViewUsersAsync(request.PageNumber, 5);
            return result switch
            {
                "UsersNotFound" => NotFound(SharedResponseKeys.UsersNotFound),
                "UsersFound" => Success(users, message: SharedResponseKeys.UsersFound),
                _ => NotFound(SharedResponseKeys.UsersNotFound)
            };
        }
    }
}
