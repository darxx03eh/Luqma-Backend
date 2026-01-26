# 🍽 LUQMA 👩🏻‍🍳 \*ੈ✩‧

[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-68217A?style=for-the-badge&logo=efcore&logoColor=white)](https://learn.microsoft.com/ef/)
[![LUQMA](https://img.shields.io/badge/LUQMA-FF6600?style=for-the-badge&logoColor=white)](#)

### Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Prerequisites](#️-prerequisites)
- [Running the App](#-running-the-app)
- [API Documentation](#-api-documentation)
- [Folder Structure](#-folder-structure)
- [Authentication & Authorization](#-authentication--authorization)
- [Features](#-features)
- [Database Schema](#-database-schema)
- [Authors](#-authors)

## 📋 Overview

Luqma is a smart restaurant management platform designed to digitize and streamline daily restaurant operations through a scalable and secure backend API. The system provides comprehensive tools for managing users, orders, payments, inventory, and reporting, while ensuring high performance and maintainability through Clean Architecture and CQRS principles. It supports secure authentication, role-based access control, email and WhatsApp notifications, media management, and financial tracking. Luqma is built to integrate seamlessly with AI-driven services to enhance decision-making, reduce food waste, and support data-driven restaurant operations.

---

## </> Tech Stack

| Layer          | Technology                                         |
| -------------- | -------------------------------------------------- |
| Backend        | ASP.NET Core 8 (Web API)                           |
| ORM            | Entity Framework Core 8                            |
| Database       | SQL Server                                         |
| Auth           | JWT Bearer Authentication                          |
| Validation     | FluentValidation                                   |
| Object Mapping | AutoMapper                                         |
| Email Service  | MailKit                                            |
| Architecture   | Clean Architecture With CQRS and Mediator Patterns |

---

## 🧭 Architecture

This project follows **Clean Architecture**, which provides a separation of concerns through layered structuring:

- `Luqma.API`: Presentation layer exposing RESTful APIs
- `Luqma.Core`: Middlewares, Behaviors, Exceptions and Features Organized by Mediator
- `Luqma.Data`: Entities, Response, Routing and DTOs
- `Luqma.Infrastructure`: Database, Repository and Database Configurations
- `Luqma.Service`: Business logic and external services

Why this helps:

- Clear separation of concerns
- Easy testing & maintainability
- Scalable as modules grow (inventory/orders/users/etc.)

---

## 🛠️ Prerequisites

To run this project locally:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022+ or CLI

---

## 🚀 Running the App

1. **Clone the repository**:

   ```bash
   git clone https://github.com/darxx03eh/Luqma-Backend.git
   cd Luqma-Backend
   ```

2. **Configure the connection string** in `appsettings.json` under the `WebAPI` project:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "LuqmaConnection": "YOUR-CONNECTION-STRING"
  },
  "JwtSettings": {
    "Issuer": "YOUR-ISSUER",
    "Audience": "YOUR-AUDIENCE",
    "SecretKey": "SECRETKEY",
    "ValidateAudience": true,
    "ValidateIssuer": true,
    "ValidateLifetime": true,
    "ValidateIssuerSigningKey": true,
    "AccessTokenExpireDate": 1,
    "RefreshTokenExpireDate": 20,
    "CustomerTokenExpireDate": 1
  },
  "EmailSettings": {
    "FromEmail": "YOUR-EMAIL",
    "Password": "PASSWORD",
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "UseSSL": false
  },
  "WhatsAppSettings": {
    "AccountSid": "YOUR-ACCOUNTSID",
    "AuthToken": "YOUR-TOKEN",
    "FromNumber": "+14155238886"
  },
  "CloudinarySettings": {
    "CloudName": "CLOUD-NAME",
    "ApiKey": "APIKEY",
    "ApiSecret": "APISECRET"
  },
  "EncryptionSettings": {
    "Key": "YOUR-ENCRYPTION-KEY"
  },
  "OpenWeather": {
    "ApiKey": "YOUR_OPENWEATHER_API_KEY"
  },
  "Stripe": {
    "StripeKey": "YOUR_STRIPE_KEY"
  }
}
```

3. **Apply Migrations**:

```bash
dotnet ef database update --project Luqma.Infrastructure
```

4. **Run the application** (WebAPI only):

   ```bash
   dotnet run --project Luqma.API
   ```

5. **Browse to Swagger UI**:

   ```
   https://localhost:<port>/swagger
   ```

---

## 📚 API Documentation

Swagger UI is enabled by default. After running the project, navigate to:

```
https://localhost:<port>/swagger
```

**Provided Soon**

---

## 🗂 Folder Structure

```

+---src
|
+---Luqma.API
|   |   appsettings.Development.json
|   |   appsettings.json
|   |   libman.json
|   |   Luqma.API.csproj
|   |   Luqma.API.csproj.Backup.tmp
|   |   Luqma.API.csproj.user
|   |   Luqma.API.http
|   |   Program.cs
|   |
|   +---Areas
|   |   +---Cashier
|   |   |   \---Controllers
|   |   |           OrderItemsController.cs
|   |   |           OrdersController.cs
|   |   |
|   |   +---Chef
|   |   |   \---controllers
|   |   |           OrderItemsController.cs
|   |   |           OrdersController.cs
|   |   |
|   |   +---Customer
|   |   |   \---Controllers
|   |   |           CartsController.cs
|   |   |           CategoriesController.cs
|   |   |           CategoryItemsController.cs
|   |   |           CustomersController.cs
|   |   |           MenuContainsController.cs
|   |   |           MenusController.cs
|   |   |           OrdersController.cs
|   |   |           PaymentsController.cs
|   |   |
|   |   +---Delivery
|   |   |   \---Controllers
|   |   |           OrdersController.cs
|   |   |
|   |   \---Manager
|   |       \---Controllers
|   |               CategoriesController.cs
|   |               MenuItemsController.cs
|   |               MenusController.cs
|   |
|   +---Base
|   |       AppBaseController.cs
|   |
|   +---Controllers
|   |       AuthenticationsController.cs
|   |       BillsController.cs
|   |       DeductionsController.cs
|   |       FeedbacksController.cs
|   |       KitchenItemsController.cs
|   |       KitchenRequirementsController.cs
|   |       OrderTrackingsController.cs
|   |       SalariesController.cs
|   |       UsersController.cs
|   |
|   |
|   \---Templates
|           ConfirmationEmail.html
|           ForgetPassword.html
|
+---Luqma.Core
|   |   Luqma.Core.csproj
|   |   ModuleCoreServices.cs
|   |
|   +---Bases
|   |       ApiResponse.cs
|   |       ApiResponseHandler.cs
|   |       FileApiResponse.cs
|   |
|   +---Behaviors
|   |       ValidationBehavior.cs
|   |
|   +---Exceptions
|   |       CustomValidationException.cs
|   |
|   +---Features
|   |   +---Authentications
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       AuthenticationCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       ConfirmationEmailCommand.cs
|   |   |   |   |       ConfirmationPhoneNumberCommand.cs
|   |   |   |   |       ForgetPasswordConfirmationCommand.cs
|   |   |   |   |       GenerateRefreshTokenCommand.cs
|   |   |   |   |       ResetPasswordCommand.cs
|   |   |   |   |       RevokeRefreshTokenCommand.cs
|   |   |   |   |       SendConfirmationCodeThenAddCommand.cs
|   |   |   |   |       SendConfirmationEmailCommand.cs
|   |   |   |   |       SendForgetPasswordCommand.cs
|   |   |   |   |       SignInCommand.cs
|   |   |   |   |       SignUpCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           ConfirmationEmailValidator.cs
|   |   |   |           ConfirmationPhoneNumberValidator.cs
|   |   |   |           ForgetPasswordConfirmationValidator.cs
|   |   |   |           ResetPasswordValidator.cs
|   |   |   |           SendConfirmationCodeThenAddValidator.cs
|   |   |   |           SendConfirmationEmailValidator.cs
|   |   |   |           SendForgetPasswordValidator.cs
|   |   |   |           SignInValidator.cs
|   |   |   |           SignUpValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       AuthenticationQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               ValidateAccessTokenQuery.cs
|   |   |
|   |   +---Bills
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       BillCommandHandler.cs
|   |   |   |   |
|   |   |   |   \---Models
|   |   |   |           AddNewBillCommand.cs
|   |   |   |           DeleteBillCommand.cs
|   |   |   |           UpdateBillCommand.cs
|   |   |   |           UpdateBillStatusCommand.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       BillQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetBillsQuery.cs
|   |   |
|   |   +---Carts
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       CartCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddToCartCommand.cs
|   |   |   |   |       DecreaseQuantityCommand.cs
|   |   |   |   |       DeleteItemFromCartForCustomer.cs
|   |   |   |   |       IncreaseQuantityCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddToCartValidator.cs
|   |   |   |           DecreaseQuantityValidator.cs
|   |   |   |           IncreaseQuantityValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       CartQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetCartforCustomer.cs
|   |   |
|   |   +---Categories
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       CategoryCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddCategoryCommand.cs
|   |   |   |   |       DeleteCategoryCommand.cs
|   |   |   |   |       UpdateCategoryCommand.cs
|   |   |   |   |
|   |   |   |   \---validators
|   |   |   |           AddCategoryValidator.cs
|   |   |   |           DeleteCategoryValidator.cs
|   |   |   |           UpdateCategoryValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       CategoryQueryHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       GetAllCategoryQuery.cs
|   |   |       |       GetCategoryQuery.cs
|   |   |       |
|   |   |       \---validators
|   |   |               GetCategoryValidator.cs
|   |   |
|   |   +---CategoryItems
|   |   |   \---Queries
|   |   |       +---Handler
|   |   |       |       CategoryItemQueryHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       GetallMenuItems.cs
|   |   |       |       GetItemsbyCategoryIdQuery.cs
|   |   |       |
|   |   |       \---validators
|   |   |               GetItemsbyCategoryQueryValidator.cs
|   |   |
|   |   +---Customers
|   |   |   +---commands
|   |   |   |   +---Handlers
|   |   |   |   |       CustomerCommandHandler.cs
|   |   |   |   |
|   |   |   |   \---Models
|   |   |   |           AddPhoneNumberCommand.cs
|   |   |   |           ConfirmPhoneNumberCodeCommand.cs
|   |   |   |           UpdateCustomerDetailsCommand.cs
|   |   |   |
|   |   |   +---Queries
|   |   |   |   +---Handlers
|   |   |   |   |       CustomerQueryHandler.cs
|   |   |   |   |
|   |   |   |   \---Models
|   |   |   |           GetCustomerInformationQuery.cs
|   |   |   |
|   |   |   \---validators
|   |   |           AddPhoneNumberValidator.cs
|   |   |           ConfirmPhoneNumberValidator.cs
|   |   |           UpdateCustomerValidator.cs
|   |   |
|   |   +---Deductions
|   |   |   +---commands
|   |   |   |   +---Handlers
|   |   |   |   |       DeductionCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddDeductionToUserCommand.cs
|   |   |   |   |       RemoveDeductionFromUserCommand.cs
|   |   |   |   |       UpdateDeductionCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddDeductionToUserValidator.cs
|   |   |   |           UpdateDeductionValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       DeductionQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               ShowAllDeductionsQuery.cs
|   |   |               ShowDeductionsForSpecificUserQuery.cs
|   |   |               ShowDeductionsForSpecificYearAndMonthQuery.cs
|   |   |
|   |   +---Feebacks
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       FeedbackCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddNewFeedbackCommand.cs
|   |   |   |   |       DeleteExistingFeedbackCommand.cs
|   |   |   |   |       UpdateExistingFeedbackCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddNewFeedbackValidator.cs
|   |   |   |           UpdateExistingFeedbackValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       FeedbackQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetFeedbacksForItemCommand.cs
|   |   |
|   |   +---KitchenItems
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       KitchenItemsCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddKitchenItemsCommand.cs
|   |   |   |   |       DeleteKitchenItemsCommand.cs
|   |   |   |   |       UpdateKitchenItemsCommand.cs
|   |   |   |   |       UpdateKitchenItemsStatusCommand.cs
|   |   |   |   |       UploadNewKitchenItemImageCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddKitchenItemsValidator.cs
|   |   |   |           UpdateKitchenItemsStatusValidator.cs
|   |   |   |           UpdateKitchenItemsValidator.cs
|   |   |   |           UploadNewKitchenItemImageValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       KitchenItemsQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetKitchenItemByIdQuery.cs
|   |   |               GetKitchenItemsQuery.cs
|   |   |
|   |   +---KitchenRequirements
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       KitchenRequirementsCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       ChangeKitchenRequirementsStatusCommand.cs
|   |   |   |   |       DeleteKitchenRequirementsCommand.cs
|   |   |   |   |       DeletePendingRequirementsCommand.cs
|   |   |   |   |       PlaceNewKitchenRequirementsCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           ChangeKitchenRequirmentsStatusValidator.cs
|   |   |   |           PlaceNewKitchenRequirmentsValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       KitchenRequirementsQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetKitchenRequirementsByIdQuery.cs
|   |   |               GetKitchenRequirementsInfoQuery.cs
|   |   |               GetKitchenRequirementsQuery.cs
|   |   |
|   |   +---MenuContains
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       MenuContainsQueryHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       GetitemsbyMenuIdQuery.cs
|   |   |       |
|   |   |       \---Validators
|   |   |               GetitemsbyMenuIdValidator.cs
|   |   |
|   |   +---MenuItems
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       MenuItemCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddMenuItemCommand.cs
|   |   |   |   |       DeleteMenuItemCommand.cs
|   |   |   |   |       ToggleStatusCommand.cs
|   |   |   |   |       UpdateMenuItemCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddMenuitemValidator.cs
|   |   |   |           DeleteMenuItemValidator.cs
|   |   |   |           ToggleStatusValidator.cs
|   |   |   |           UpdateMenuItemValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       MenuItemQueryHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       GetAllMenuItemQuery.cs
|   |   |       |       GetMenuItemByIdQuery.cs
|   |   |       |
|   |   |       \---Validators
|   |   |               GetByIdValidator.cs
|   |   |
|   |   +---Menus
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       MenuCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddMenuCommand.cs
|   |   |   |   |       DeleteMenuCommand.cs
|   |   |   |   |       UpdateMenuCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddMenuValidator.cs
|   |   |   |           DeleteMenuValidator.cs
|   |   |   |           UpdateMenuValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       MenuQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetAllMenusQuery.cs
|   |   |
|   |   +---OrderItems
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       OrderItemCommandHandler.cs
|   |   |   |   |
|   |   |   |   \---Models
|   |   |   |           AddItemToOrderCommand.cs
|   |   |   |           DeleteItemFromOrderCommand.cs
|   |   |   |           UpdateQuantityForItemByCashierCommand.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       OrderItemQueryHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       GetOrderReportQuery.cs
|   |   |       |       ViewOrderDetailsQuery.cs
|   |   |       |       ViewOrdersQuery.cs
|   |   |       |
|   |   |       \---Validators
|   |   |               ViewOrderDetailsValidator.cs
|   |   |
|   |   +---Orders
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       OrderCommandHandler.cs
|   |   |   |   |
|   |   |   |   +---Models
|   |   |   |   |       AddOrderCommand.cs
|   |   |   |   |       CancelOrder.cs
|   |   |   |   |       CancelOrderByCashierCommand.cs
|   |   |   |   |       ChangeStatusByChefCommand.cs
|   |   |   |   |       ChangeStatusByDeliveryToDeliveredCommand.cs
|   |   |   |   |       ChangeStatusToOutByDeliveryCommand.cs
|   |   |   |   |       placeOrderCommand.cs
|   |   |   |   |       UpdateOnOrderTotalPriceCommand.cs
|   |   |   |   |       UpdateOrderByCashierCommand.cs
|   |   |   |   |
|   |   |   |   \---Validators
|   |   |   |           AddOrderValidator.cs
|   |   |   |           UpdateOrderByCashierValidator.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       OrderQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               ViewOrderDetailsByDeliveryQuery.cs
|   |   |               ViewOrdersByChefQuery.cs
|   |   |               ViewOrdersByDeliveryQuery.cs
|   |   |               viewOutOrdersforDeliveryQuery.cs
|   |   |
|   |   +---OrdersTracking
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       OrderTrackingQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               OrderTrackingQuery.cs
|   |   |
|   |   +---Payments
|   |   |   \---Commands
|   |   |       +---Handlers
|   |   |       |       PaymentCommandHandler.cs
|   |   |       |
|   |   |       +---Models
|   |   |       |       AddPaymentCommand.cs
|   |   |       |       SuccessPaymentCommand.cs
|   |   |       |
|   |   |       \---Validators
|   |   |               AddPaymentValidator.cs
|   |   |
|   |   +---Salaries
|   |   |   +---Commands
|   |   |   |   +---Handlers
|   |   |   |   |       SalaryCommandHandler.cs
|   |   |   |   |
|   |   |   |   \---Models
|   |   |   |           ChangeSalaryAmountCommand.cs
|   |   |   |           ChangeSalaryStatusCommand.cs
|   |   |   |           DeleteSalaryCommand.cs
|   |   |   |           GenerateSalariesCommand.cs
|   |   |   |           GenerateSalaryForUserCommand.cs
|   |   |   |
|   |   |   \---Queries
|   |   |       +---Handlers
|   |   |       |       SalaryQueryHandler.cs
|   |   |       |
|   |   |       \---Models
|   |   |               GetSalariesQuery.cs
|   |   |
|   |   \---Users
|   |       +---Commands
|   |       |   +---Handlers
|   |       |   |       UserCommandHandler.cs
|   |       |   |
|   |       |   +---Models
|   |       |   |       ActivateUserCommand.cs
|   |       |   |       AddUserAddressCommand.cs
|   |       |   |       ChangeBirthDateCommand.cs
|   |       |   |       ChangeNameCommand.cs
|   |       |   |       ChangePasswordCommand.cs
|   |       |   |       ChangePasswordForUserByManagerCommand.cs
|   |       |   |       ChangeSalaryCommand.cs
|   |       |   |       ChangeUserNameCommand.cs
|   |       |   |       ChangeUserRolesCommand.cs
|   |       |   |       DeactivateUserCommand.cs
|   |       |   |       DeleteProfileImageCommand.cs
|   |       |   |       DeleteUserAddressCommand.cs
|   |       |   |       UpdateUserAddressCommand.cs
|   |       |   |       UpdateUserDataCommand.cs
|   |       |   |       UploadProfileImageCommand.cs
|   |       |   |
|   |       |   \---Validators
|   |       |           AddUserAddressValidator.cs
|   |       |           ChangeBirthDateValidator.cs
|   |       |           ChangeNameValidator.cs
|   |       |           ChangePasswordForUserByManagerValidator.cs
|   |       |           ChangePasswordValidator.cs
|   |       |           ChangeSalaryValidator.cs
|   |       |           ChangeUserNameValidator.cs
|   |       |           UpdateUserAddressValidator.cs
|   |       |           UpdateUserDataValidator.cs
|   |       |           UploadProfileImageValidator.cs
|   |       |
|   |       \---Queries
|   |           +---Handlers
|   |           |       UserQueryHandler.cs
|   |           |
|   |           \---Models
|   |                   GetUserProfileQuery.cs
|   |                   GetUsersForFinanceQuery.cs
|   |                   ShowUserAddressesQuery.cs
|   |                   ViewSpecificAddressQuery.cs
|   |                   ViewUsersQuery.cs
|   |
|   +---Mapping
|   |   +---Authentications
|   |   |   |   AuthenticationProfile.cs
|   |   |   |
|   |   |   \---CommandMapping
|   |   |           SignUpMapping.cs
|   |   |
|   |   +---Carts
|   |   |       CartProfile.cs
|   |   |
|   |   +---Categories
|   |   |       CategoryProfile.cs
|   |   |
|   |   +---CategoryItems
|   |   |       CategoryItemProfile.cs
|   |   |
|   |   +---Customers
|   |   |       CustomerProfile.cs
|   |   |
|   |   +---MenuContain
|   |   |       MenuContainProfile.cs
|   |   |
|   |   +---MenuItems
|   |   |       MenuItemProfile.cs
|   |   |
|   |   +---Menus
|   |   |       MenuProfile.cs
|   |   |
|   |   +---Orders
|   |   |       OrderProfile.cs
|   |   |
|   |   \---Users
|   |       |   UserProfile.cs
|   |       |
|   |       \---QueryMapping
|   |               GetUsersForFinanceMapping.cs
|   |
|   +---Middlewares
|   |       ErrorHandlerMiddleWare.cs
|   |       TokenValidationMiddleware.cs
|   \---ResponseKeys
|           SharedResponseKeys.cs
|
+---Luqma.Data
|   |   Luqma.Data.csproj
|   |   ModuleDataServices.cs
|   |
|   +---DTOs
|   |   +---RequirementItems
|   |   |       RequirementItemsDTO.cs
|   |   |
|   |   \---Users
|   |           UpdateUserDataDTO.cs
|   |           UserRolesDTO.cs
|   |
|   +---Entities
|   |   |   Bill.cs
|   |   |   Cart.cs
|   |   |   Category.cs
|   |   |   CategoryItem.cs
|   |   |   Customer.cs
|   |   |   CustomerAddress.cs
|   |   |   Deduction.cs
|   |   |   Deliveries.cs
|   |   |   Feedback.cs
|   |   |   KitchenItems.cs
|   |   |   KitchenRequirements.cs
|   |   |   Menu.cs
|   |   |   MenuContains.cs
|   |   |   MenuItem.cs
|   |   |   Order.cs
|   |   |   OrderItem.cs
|   |   |   OrderTracking.cs
|   |   |   Payment.cs
|   |   |   PaymentsOrder.cs
|   |   |   Prediction.cs
|   |   |   RequirementItems.cs
|   |   |   Salary.cs
|   |   |   UserAddress.cs
|   |   |   WasteReport.cs
|   |   |
|   |   \---Identity
|   |           LuqmaRole.cs
|   |           LuqmaUser.cs
|   |           UserRefreshToken.cs
|   |
|   +---Enums
|   |       Gender.cs
|   |
|   +---Helpers
|   |       CloudinarySettings.cs
|   |       EmailSettings.cs
|   |       EncryptionHelper.cs
|   |       EncryptionSettings.cs
|   |       JwtSettings.cs
|   |       OpenWeatherSettings.cs
|   |       Roles.cs
|   |       UserClaimModel.cs
|   |       WhatsAppSettings.cs
|   +---Response
|   |   +---Authentications
|   |   |       SignInResponse.cs
|   |   |
|   |   +---Bills
|   |   |       BillsResponse.cs
|   |   |
|   |   +---Carts
|   |   |       CartResponse.cs
|   |   |
|   |   +---Categories
|   |   |       CategoryResponse.cs
|   |   |
|   |   +---CategoryItems
|   |   |       CategoryItemResponse.cs
|   |   |
|   |   +---Customers
|   |   |       CustomerAddressResponse.cs
|   |   |       CustomerResponse.cs
|   |   |
|   |   +---Deductions
|   |   |       TotalDeductionsResponse.cs
|   |   |       ViewDeductionsResponse.cs
|   |   |
|   |   +---Feedbacks
|   |   |       AddNewFeedbackResponse.cs
|   |   |       GetFeedbacksForItemResponse.cs
|   |   |
|   |   +---KitchenItems
|   |   |       GetKitchenItemsResponse.cs
|   |   |
|   |   +---KitchenRequirements
|   |   |       GetKitchenRequirementsInfoResponse.cs
|   |   |       GetKitchenRequirementsResponse.cs
|   |   |
|   |   +---MenuContains
|   |   |       MenuContainsRespons.cs
|   |   |
|   |   +---MenuItems
|   |   |       MenuItemResponse.cs
|   |   |
|   |   +---Menus
|   |   |       MenuResponse.cs
|   |   |
|   |   +---Order
|   |   |       OrderResponse.cs
|   |   |       ViewOrderDetailsByDeliveryResponse.cs
|   |   |       ViewOrderDetailsResponse.cs
|   |   |       ViewOrderResponse.cs
|   |   |
|   |   +---OrdersTracking
|   |   |       OrderTrackingResponse.cs
|   |   |
|   |   +---Salaries
|   |   |       GetSalariesResponse.cs
|   |   |
|   |   \---Users
|   |           GetUsersForFinanceResponse.cs
|   |           ProfileResponse.cs
|   |           ShowUserAddressResponse.cs
|   |           ViewUsersResponse.cs
|   |
|   +---Routing
|   |       Router.cs
|   |
|   \---Wrappers
|           PaginatedResult.cs
|           QueryableExtensions.cs
|
+---Luqma.Infrastructure
|   |   Luqma.Infrastructure.csproj
|   |   ModuleInfrastructureServices.cs
|   |
|   +---Configurations
|   |       BillConfigurations.cs
|   |       CartConfigurations.cs
|   |       CategoryConfigurations.cs
|   |       CategoryItemConfigurations.cs
|   |       CustomerAddressConfigurations.cs
|   |       CustomerConfigurations.cs
|   |       DeductionConfigurations.cs
|   |       DeliveriesConfigurations.cs
|   |       FeedbackConfigurations.cs
|   |       KitchenItemsConfigurations.cs
|   |       KitchenRequirmentsConfigurations.cs
|   |       LuqmaUserConfigurations.cs
|   |       MenuConfigurations.cs
|   |       MenuContainsConfigurations.cs
|   |       MenuItemConfigurations.cs
|   |       OrderConfigurations.cs
|   |       OrderItemConfigurations.cs
|   |       OrderTrackingConfigurations.cs
|   |       PaymentConfigurations.cs
|   |       PaymentsOrderConfigurations.cs
|   |       PredictionConfigurations.cs
|   |       RequirmentItemsConfigurations.cs
|   |       SalaryConfigurations.cs
|   |       UserAddressConfigurations.cs
|   |       UserRefreshTokenConfigurations.cs
|   |       WasteReportConfigurations.cs
|   |
|   +---Data
|   |       LuqmaDbContext.cs
|   |
|   +---IRepositories
|   |       IBillRepository.cs
|   |       ICartRepository.cs
|   |       ICategoryItemRepository.cs
|   |       ICategoryRepository.cs
|   |       ICustomerRepository.cs
|   |       IDeductionRepository.cs
|   |       IDeliveriesRepository.cs
|   |       IFeedbackRepository.cs
|   |       IGenericRepository.cs
|   |       IKitchenItemsRepository.cs
|   |       IKitchenRequirementsRepository.cs
|   |       IMenuContainsRepository.cs
|   |       IMenuItemRepository.cs
|   |       IMenuRepository.cs
|   |       IOrderItemRepository.cs
|   |       IOrderRepository.cs
|   |       IOrderTrackingRepository.cs
|   |       IPaymentOrderRepository.cs
|   |       IPaymentRepository.cs
|   |       IRefreshTokenRepository.cs
|   |       IRequirementItemsRepository.cs
|   |       ISalaryRepository.cs
|   |       IUnitOfWork.cs
|   |       IUserAddressRepository.cs
|   |       IUserRepository.cs
|   |
|   +---Repositories
|   |       BillRepository.cs
|   |       CartRepository.cs
|   |       CategoryItemRepository.cs
|   |       CategoryRepository.cs
|   |       CustomerRepository.cs
|   |       DeductionRepository.cs
|   |       DeliveriesRepository.cs
|   |       FeedbackRepository.cs
|   |       GenericRepository.cs
|   |       KitchenItemsRepository.cs
|   |       KitchenRequirementsRepository.cs
|   |       MenuContainsRepository.cs
|   |       MenuItemRepository.cs
|   |       MenuRepository.cs
|   |       OrderItemRepository.cs
|   |       OrderRepository.cs
|   |       OrderTrackingRepository.cs
|   |       PaymentOrderRepository.cs
|   |       PaymentRepository.cs
|   |       RefreshTokenRepository.cs
|   |       RequirementItemsRepository.cs
|   |       SalaryRepository.cs
|   |       UnitOfWork.cs
|   |       UserAddressRepository.cs
|   |       UserRepository.cs
|   |
|   \---Seeder
|           RoleSeeder.cs
|           UserSeeder.cs
|
\---Luqma.Service
    |   Luqma.Service.csproj
    |   ModuleServiceServices.cs
    +---Implementations
    |       AuthenticationService.cs
    |       BillService.cs
    |       CartService.cs
    |       CategoryItemService.cs
    |       CategoryService.cs
    |       CloudinaryService.cs
    |       CustomerService.cs
    |       DeductionService.cs
    |       EmailService.cs
    |       FeedbackService.cs
    |       FileService.cs
    |       KitchenItemsService.cs
    |       KitchenRequirementsService.cs
    |       MenuContainService.cs
    |       MenuItemService.cs
    |       MenuService.cs
    |       OrderItemService.cs
    |       OrderService.cs
    |       OrderTrackingService.cs
    |       PaymentService.cs
    |       SalaryService.cs
    |       StripeSettings.cs
    |       TokenService.cs
    |       UserService.cs
    |       WeatherService.cs
    |       WhatsAppService.cs
    |
    +---Interfaces
    |       IAuthenticationService.cs
    |       IBillService.cs
    |       ICartService.cs
    |       ICategoryItemService.cs
    |       ICategoryService.cs
    |       ICloudinaryService.cs
    |       ICustomerService.cs
    |       IDeductionService.cs
    |       IEmailService.cs
    |       IFeedbackService.cs
    |       IFileService.cs
    |       IKitchenItemsService.cs
    |       IKitchenRequirementsService.cs
    |       IMenuContainService.cs
    |       IMenuItemService.cs
    |       IMenuService.cs
    |       IOrderItemService.cs
    |       IOrderService.cs
    |       IOrderTrackingService.cs
    |       IPaymentService.cs
    |       ISalaryService.cs
    |       ITokenService.cs
    |       IUserService.cs
    |       IWeatherService.cs
    |       IWhatsAppService.cs

```

---

## 🔒 Authentication & Authorization

- Uses **JWT Tokens**
- Supports roles: `Manager`, `Chef`, `Cashier`, `Finance`, `Delivery`, `Customer`
- Role-based access via `[Authorize(Roles = "Manager")]`

---

## 📧 Features

- ✅ ASP.NET Core with SQL Server
- 🧱 Clean Architecture, CQRS, and MediatR
- 🔐 JWT Authentication & Role-Based Authorization
- 📧 Email confirmation using MailKit
- 👤 User management
- 💳 Payment handling (Cash & Visa)
- 🖼️ Image management via Cloudinary
- 🛡️ Global Error Handling Middleware
- 📊 Reports generation for orders, inventory, and finance
- 🧩 DTOs, AutoMapper, validation with Pipeline Behaviors
- 💬 WhatsApp messaging via Twilio

---

## 🖧 Database Schema

Below is the Entity-Relationship Diagram (ERD) illustrating the structure of the database, including tables, relationships, and keys used in the application.

![ERD Diagram](ERD/Luqma.svg)

## 🪶 Authors

Built by

### 👨‍💻 Mahmoud Darawsheh - `darxx03eh`

email: darxx03eh@gmail.com  
[Mahmoud Darawsheh - linkedin](https://www.linkedin.com/in/mahmoud-darawsheh)  
[Mahmoud Darawsheh - github](https://github.com/darxx03eh)

---

### 👨‍💻 Thamer Draidi - `ThamerDraidi`

email: thamerdraidi@gmail.com  
[Thamer Draidi - linkedin](https://www.linkedin.com/in/thamer-draidi-145676317)  
[Thamer Draidi - github](https://github.com/ThamerDraidi)

---

<p align="center"> ©2026 Luqma Restaurant. All rights reserved.</p>
<p align="center">Built with ☯︎ by Mahmoud Darawsheh & Thamer Draidi</p>
