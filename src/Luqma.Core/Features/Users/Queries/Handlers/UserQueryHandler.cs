using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Users.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Users;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ApiResponseHandler
        , IRequestHandler<ViewUsersQuery, ApiResponse>
        , IRequestHandler<ShowUserAddressesQuery, ApiResponse>
        , IRequestHandler<ViewSpecificAddressQuery, ApiResponse>
        , IRequestHandler<GetUserProfileQuery, ApiResponse>
        , IRequestHandler<GetUsersForFinanceQuery, ApiResponse>
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserQueryHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
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

        public async Task<ApiResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var (result, profile) = await userService.GetUserProfileAsync(request.UserName);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "UserFound" => Success(profile, message: SharedResponseKeys.UsersFound),
                "ThereWasAProblemLoadingTheProfile" => InternalServerError(SharedResponseKeys.ThereWasAProblemLoadingTheProfile),
                _ => InternalServerError(SharedResponseKeys.ThereWasAProblemLoadingTheProfile)
            };
        }

        public async Task<ApiResponse> Handle(GetUsersForFinanceQuery request, CancellationToken cancellationToken)
        {
            var (result, users) = await userService.GetUsersForFinanceAsync();
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "UsersNotFound" => NotFound(SharedResponseKeys.UsersNotFound),
                "UsersFound" => Success(mapper.Map<IList<GetUsersForFinanceResponse>>(users), message: SharedResponseKeys.UsersFound),
                _ => NotFound(SharedResponseKeys.UsersNotFound)
            };
        }
    }
}