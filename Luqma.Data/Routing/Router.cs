namespace Luqma.Data.Routing
{
    public static class Router
    {
        public const string singleRoute = "/{id}";
        public const string root = "api";
        public const string version = "v1";
        public const string rule = $"{root}/{version}/";
        public static class AuthenticationsRouting
        {
            public const string prefix = $"{rule}authentications";
            public const string SignUp = $"{prefix}/register";
            public const string EmailConfirmation = $"{prefix}/email-confirmation";
            public const string SignIn = $"{prefix}/login";
            public const string SendConfirmationEmail = $"{prefix}/send-confirmation-email";
            public const string ValidateAccessToken = $"{prefix}/token-validate";
            public const string GenerateRefreshToken = $"{prefix}/refresh-token";
            public const string RevokeRefreshToken = $"{prefix}/refresh-token"; 
            public const string SendForgetPasswordEmail = $"{prefix}/send-forget-password-email";
            public const string ForgetPasswordConfirmation = $"{prefix}/forget-password-confirmation";
            public const string ResetPassword = $"{prefix}/reset-password"; 
            public const string SendConfirmationCodeThenAdd = $"{prefix}/send-confirmation-code-add";
            public const string PhoneNumberConfirmation = $"{prefix}/phonenumber-confirmation";
        }
        public static class UsersRouting
        {
            public const string prefix = $"{rule}users";
            public const string ChangePassword = $"{prefix}/change-password";
            public const string ChangeName = $"{prefix}/change-name";
            public const string UploadProfileImage = $"{prefix}/profile/upload-image";
            public const string ChangeUserName = $"{prefix}/change-username";
            public const string DeleteProfileImage = $"{prefix}/profile-image";
            public const string ChangeBirthDate = $"{prefix}/change-birth-date";
        }
    }
}
