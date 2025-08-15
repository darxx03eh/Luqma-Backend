using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Bases
{
    public class ApiResponseHandler
    {
        public ApiResponse InternalServerError(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.InternalServerError : message
            };
        public ApiResponse Conflict(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.Conflict : message
            };
        public ApiResponse NoContent(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Succeeded = true,
                Message = message is null ? SharedResponseKeys.NoContent : message
            };
        public ApiResponse Deleted(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message is null ? SharedResponseKeys.Deleted : message
            };
        public ApiResponse Locked(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.Locked,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.TheResourceThatIsBeingAccessedIsLocked : message
            };
        public ApiResponse Success(Object entity, Object meta = null, String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = entity,
                Meta = meta,
                Succeeded = true,
                Message = message is null ? SharedResponseKeys.Success : message
            };
        public ApiResponse Unauthorized(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.Unauthorized : message
            };
        public ApiResponse Forbidden(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.Forbidden: message
            };
        public ApiResponse BadRequest(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.BadRequest : message
            };
        public ApiResponse UnprocessableEntity(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.UnprocessableEntity : message
            };
        public ApiResponse NotFound(String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message is null ? SharedResponseKeys.NotFound : message
            };
        public ApiResponse Created(Object entity, Object meta = null, String message = null)
            => new ApiResponse()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Data = entity,
                Meta = meta,
                Message = message is null ? SharedResponseKeys.Created : message
            };
    }
}
