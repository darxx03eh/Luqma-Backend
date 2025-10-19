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
            public const string UserName = "/{username}";
            public const string prefix = $"{rule}users";
            public const string settings = $"settings";
            public const string ChangePassword = $"{prefix}/{settings}/change-password";
            public const string ChangeName = $"{prefix}/{settings}/change-name";
            public const string UploadProfileImage = $"{prefix}/{settings}/profile/upload-image";
            public const string ChangeUserName = $"{prefix}/{settings}/change-username";
            public const string DeleteProfileImage = $"{prefix}/{settings}/profile-image";
            public const string ChangeBirthDate = $"{prefix}/{settings}/change-birth-date";

            public const string DeactiveUser = $"{prefix}/deactive";
            public const string ActivateUser = $"{prefix}/active";
            public const string ViewUsers = $"{prefix}";
            public const string UserAddress = $"{prefix}/address";
            public const string DeleteUserAddress = $"{prefix}/address{singleRoute}";
            public const string ShowUserAddresses = $"{prefix}/address";
            public const string ShowSpecificAddress = $"{prefix}/address{singleRoute}";
            public const string Profile = $"{prefix}/profile{UserName}";
        }
        public static class ManagerCategoriesRouting
        {
            public const string prefix = $"{rule}Manager/Categories";
            public const string GetAll = $"{prefix}/GetAll";
            public const string Add = $"{prefix}/Add";
            public const string Update = $"{prefix}/Update";
            public const string Delete = $"{prefix}/Delete{singleRoute}";
            public const string GetById = $"{prefix}/GetById{singleRoute}";
        }
        public static class CategoriesRouting
        {
            public const string prefix = $"{rule}Categories";
            public const string GetAll = $"{prefix}/GetAll";
            public const string GetById = $"{prefix}/GetById{singleRoute}";
        }
      
        public static class DeductionsRouting
        {
            public const string name = "/{name}";
            public const string prefix = $"{rule}deductions";
            public const string AddDeductionToUser = $"{prefix}";
            public const string RemoveDeductionFromUser = $"{prefix}{singleRoute}";
            public const string UpdateDeduction = $"{prefix}";
            public const string ViewAllDeductions = $"{prefix}";
            public const string ViewAllDeductionsByDate = $"{prefix}/date";
            public const string ViewAllDeductionsForSpecificUser = $"{prefix}{name}";
        }
           public static class ManagerMenuItemsRouting
        {
            public const string prefix = $"{rule}Manager/MenuItems";
            public const string Add = $"{prefix}/Add";
            public const string GetAll = $"{prefix}/GetAll";
            public const string Delete = $"{prefix}/Delete{singleRoute}";
            public const string Update = $"{prefix}/Update";
        }
        public static class ManagerMenuRouting
        {
            public const string prefix = $"{rule}Manager/Menus";
            public const string Add = $"{prefix}/Add";
            public const string Delete = $"{prefix}/Delete{singleRoute}";
            public const string Update = $"{prefix}/Update";


        }
        public static class MenuRouting
        {
            public const string prefix = $"{rule}Menus";
            public const string GetAll = $"{prefix}/GetAll";

        }
        public static class SalariesRouting
        {
            public const string prefix = $"{rule}salaries";
            public const string GenerateSalaries = $"{prefix}";
            public const string GenerateSalariesForUser = $"{prefix}/user{singleRoute}";
            public const string GetSalaries = $"{prefix}";
            public const string DeleteSalary = $"{prefix}{singleRoute}";
            public const string ChangeSalaryStatus = $"{prefix}/status";
            public const string ChangeSalaryAmount = $"{prefix}/amount";
        }
        public static class CategoryItemsRouting
        {
            public const string prefix = $"{rule}CategoryItems";
            public const string GetItemsByCategoryId = $"{prefix}/GetItemsByCategoryId{singleRoute}";
        }
        public static class MenuContainsRouting
        {
            public const string prefix = $"{rule}MenuContains";
            public const string GetItemsByMenuId = $"{prefix}/GetItemsByMenuId{singleRoute}";
        }
        public static class BillsRouting
        {
            public const string prefix = $"{rule}bills";
            public const string GetBills = $"{prefix}";
            public const string AddBills = $"{prefix}";
            public const string DeleteBills = $"{prefix}{singleRoute}";
            public const string UpdateBillStatus = $"{prefix}{singleRoute}/status";
            public const string UpdateBill = $"{prefix}";
        }
        public static class CustomerRouting
        {
            public const string prefix = $"{rule}Customers";
            public const string AddPhoneNumberThenSend = $"{prefix}/AddPhoneNumberThenSend";
            public const string ConfirmPhoneNumberCode = $"{prefix}/ConfirmPhoneNumberCode";
            public const string UpdateCustomerDetails = $"{prefix}/UpdateCustomerDetails";
            

        }
        public static class KitchenItemsRouting
        {
            public const string prefix = $"{rule}kitchen-items";
            public const string GetKitchenItems = prefix;
            public const string AddKitchenItems = prefix;
            public const string GetKitchenItemById = $"{prefix}{singleRoute}";
            public const string DeleteKitchenItem = $"{prefix}{singleRoute}";
            public const string UpdateKitchenItemStatus = $"{prefix}/status";
            public const string UpdateKitchenItem = prefix;
        }
        public static class KitchenRequirmentsRouting
        {
            public const string prefix = $"{rule}kitchen-requirments";
            public const string GetPaginatedKitchenRequirements = prefix;
            public const string GetKitchenRequirementsById = $"{prefix}{singleRoute}";
            public const string PlaceNewKitchenRequirments = prefix;
            public const string ChangeKitchenRequirmentsStatus = $"{prefix}/status";
            public const string DeleteKitchenRequirments = $"{prefix}{singleRoute}";
            public const string GetKitchenRequirmentsInfo = $"{prefix}{singleRoute}/info";
        }
    }
}
