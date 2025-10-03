using Luqma.Core.Bases;
using Luqma.Core.Features.Users.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Handlers
{
    public class UsersManagementQueryHandler : ApiResponseHandler
        , IRequestHandler<ViewUsersQuery, ApiResponse>
        , IRequestHandler<ShowUserAddressesQuery, ApiResponse>
        , IRequestHandler<ViewSpecificAddressQuery, ApiResponse>
    {
        private readonly IUserService userService;

        public UsersManagementQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ApiResponse> Handle(ViewUsersQuery request, CancellationToken cancellationToken)
        {
            var (result, users) = await userService.ViewUsersAsync(request.PageNumber, 5);
            return result switch
            {
                "UsersNotFound" => NotFound(SharedResponseKeys.UsersNotFound),
                "UsersFound" => Success(users, message: SharedResponseKeys.UsersFound),
                _ => NotFound(SharedResponseKeys.UsersNotFound)
            };
        }

        public async Task<ApiResponse> Handle(ShowUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var (result, addresses) = await userService.ShowUserAddressesAsync(request.PageNumber);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AddressesNotFound" => NotFound(SharedResponseKeys.AddressesNotFound),
                "AddressesFound" => Success(addresses, message: SharedResponseKeys.AddressesFound),
                _ => NotFound(SharedResponseKeys.AddressesNotFound)
            };
        }

        public async Task<ApiResponse> Handle(ViewSpecificAddressQuery request, CancellationToken cancellationToken)
        {
            var (result, address) = await userService.ViewSpecificAddressAsync(request.Id);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.AddressNotFound),
                "AddressNotFound" => NotFound(SharedResponseKeys.AddressNotFound),
                "ThisAddressDoesNotBelongToYou" => BadRequest(SharedResponseKeys.ThisAddressDoesNotBelongToYou),
                "AddressFound" => Success(address, message: SharedResponseKeys.AddressFound),
                _ => NotFound(SharedResponseKeys.AddressNotFound)
            };
        }
    }
}
