namespace Luqma.Data.Routing
{
    public static class Router
    {
        public const string singleRoute = "/{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string rule = $"{root}/{version}/";
        public static class AuthenticationRouting
        {
            public const string prefix = $"{rule}Authentication";
            public const string SignUp = $"{prefix}/SignUp";
            public const string EmailConfirmation = $"{prefix}/EmailConfirmation";
            public const string SignIn = $"{prefix}/SignIn";
            public const string SendConfirmationEmail = $"{prefix}/SendConfirmationEmail";
            public const string ValidateAccessToken = $"{prefix}/ValidateAccessToken";
            public const string GenerateRefreshToken = $"{prefix}/GenerateRefreshToken";
            public const string RevokeRefreshToken = $"{prefix}/RevokeRefreshToken";
            public const string SendForgetPasswordEmail = $"{prefix}/SendForgetPasswordEmail";
            public const string ForgetPasswordConfirmation = $"{prefix}/ForgetPasswordConfirmation";
            public const string ResetPassword = $"{prefix}/ResetPassword";
            public const string SendConfirmationCodeThenAdd = $"{prefix}/SendConfirmationCodeThenAdd";
            public const string PhoneNumberConfirmation = $"{prefix}/PhoneNumberConfirmation";
        }
    }
}
