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

Base API route: `api/v1`

### Authentications

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/authentications/register | Register a new user account |
| POST | /api/v1/authentications/login | Sign in and get tokens |
| GET | /api/v1/authentications/email-confirmation | Confirm email address |
| POST | /api/v1/authentications/send-confirmation-email | Resend email confirmation |
| POST | /api/v1/authentications/send-forget-password-email | Send forgot-password email |
| POST | /api/v1/authentications/forget-password-confirmation | Confirm forgot-password code |
| POST | /api/v1/authentications/reset-password | Reset account password |
| POST | /api/v1/authentications/refresh-token | Generate a new access token |
| DELETE | /api/v1/authentications/refresh-token | Revoke refresh token |
| GET | /api/v1/authentications/token-validate?token={value} | Validate access token |
| POST | /api/v1/authentications/send-confirmation-code-add | Send phone confirmation code before add |
| POST | /api/v1/authentications/phonenumber-confirmation | Confirm phone number code |

### Users

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/users/profile/{username} | Get user profile by username |
| PATCH | /api/v1/users/settings/change-password | Change current user password |
| PATCH | /api/v1/users/settings/change-name | Change current user display name |
| PATCH | /api/v1/users/settings/profile/upload-image | Upload profile image |
| PATCH | /api/v1/users/settings/change-username | Change username |
| DELETE | /api/v1/users/settings/profile-image | Delete profile image |
| PATCH | /api/v1/users/settings/change-birth-date | Change birth date |
| POST | /api/v1/users/address | Add a new user address |
| PUT | /api/v1/users/address | Update existing user address |
| DELETE | /api/v1/users/address/{id} | Delete address by id |
| GET | /api/v1/users/address?pageNumber={value} | Get paginated user addresses |
| GET | /api/v1/users/address/{id} | Get specific address by id |
| DELETE | /api/v1/users/deactive | Deactivate user |
| POST | /api/v1/users/active | Activate user |
| GET | /api/v1/users?pageNumber={value} | Get paginated users |
| PATCH | /api/v1/users/roles | Change user roles |
| PUT | /api/v1/users | Update user data |
| PATCH | /api/v1/users/user-management/password | Change password for user by manager |
| PATCH | /api/v1/users/change-salary | Change user salary |
| GET | /api/v1/users/dropdown | Get users dropdown list |

### Bills

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/bills | Add a new bill |
| DELETE | /api/v1/bills/{id} | Delete bill by id |
| PATCH | /api/v1/bills/{id}/status?status={value} | Update bill status |
| PUT | /api/v1/bills | Update bill |
| GET | /api/v1/bills?pageNumber={value} | Get paginated bills |

### Deductions

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/deductions | Add deduction to user |
| DELETE | /api/v1/deductions/{id} | Remove deduction by id |
| PUT | /api/v1/deductions | Update deduction |
| GET | /api/v1/deductions?pageNumber={value} | View all deductions (paginated) |
| GET | /api/v1/deductions/date?year={value}&month={value}&pageNumber={value} | View deductions filtered by month |
| GET | /api/v1/deductions/{name}?pageNumber={value} | View deductions for specific user |

### Feedbacks

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/feedbacks | Add new feedback |
| PUT | /api/v1/feedbacks | Update existing feedback |
| GET | /api/v1/feedbacks/item/{id}?pageNumber={value}&pageSize={value} | Get feedbacks for menu item |
| DELETE | /api/v1/feedbacks/{id} | Delete feedback by id |

### Kitchen Items

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/kitchen-items?search={value}&pageNumber={value} | Get kitchen items (paginated/search) |
| POST | /api/v1/kitchen-items | Add kitchen item |
| GET | /api/v1/kitchen-items/{id} | Get kitchen item by id |
| DELETE | /api/v1/kitchen-items/{id} | Delete kitchen item by id |
| PATCH | /api/v1/kitchen-items/status | Change kitchen item status |
| PUT | /api/v1/kitchen-items | Update kitchen item |
| PATCH | /api/v1/kitchen-items/upload-image | Upload kitchen item image |

### Kitchen Requirements

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/kitchen-requirements | Place new kitchen requirement |
| PATCH | /api/v1/kitchen-requirements/status | Change kitchen requirement status |
| GET | /api/v1/kitchen-requirements?pageNumber={value} | Get paginated kitchen requirements |
| GET | /api/v1/kitchen-requirements/{id} | Get kitchen requirement by id |
| DELETE | /api/v1/kitchen-requirements/{id} | Delete kitchen requirement by id |
| GET | /api/v1/kitchen-requirements/{id}/info | Get kitchen requirement details/info |
| DELETE | /api/v1/kitchen-requirements/pending/{id} | Delete pending kitchen requirement |

### Order Tracking

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/order-tracking/{id} | Track order by id |

### Salaries

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/salaries | Generate salaries |
| POST | /api/v1/salaries/user/{id}?year={value}&month={value} | Generate salary for specific user |
| GET | /api/v1/salaries?name={value}&status={value}&pageNumber={value}&year={value}&month={value} | Get salaries with filters |
| DELETE | /api/v1/salaries/{id} | Delete salary by id |
| PATCH | /api/v1/salaries/status | Change salary status |
| PATCH | /api/v1/salaries/amount | Change salary amount |

### Customer Area

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/Categories/GetAll | Get all categories |
| GET | /api/v1/Categories/GetById/{id} | Get category by id |
| GET | /api/v1/CategoryItems/GetItemsByCategoryId/{id} | Get items by category id |
| GET | /api/v1/CategoryItems/GetAllMenuItems | Get all menu items |
| GET | /api/v1/Menus/GetAll | Get all menus |
| GET | /api/v1/MenuContains/GetItemsByMenuId/{id} | Get menu items by menu id |
| POST | /api/v1/Customers/AddPhoneNumberThenSend | Add phone number and send code |
| POST | /api/v1/Customers/ConfirmPhoneNumberCode | Confirm phone number code |
| PUT | /api/v1/Customers/UpdateCustomerDetails | Update customer details |
| GET | /api/v1/Customers/GetCustomerInfo | Get current customer info |
| POST | /api/v1/Customer/Carts/AddTocart | Add item to customer cart |
| GET | /api/v1/Customer/Carts/GetCartForCustomer | Get customer cart |
| PATCH | /api/v1/Customer/Carts/IncreaseQuantity | Increase cart item quantity |
| PATCH | /api/v1/Customer/Carts/DecreaseQuantity | Decrease cart item quantity |
| DELETE | /api/v1/Customer/Carts/DeleteItemFormCartForCustomerByItemId/{id} | Delete item from cart by item id |
| POST | /api/v1/Customer/Orders/PlaceOrder | Place order |
| DELETE | /api/v1/Customer/Orders/CancelOrder/{id} | Cancel customer order by id |
| POST | /api/v1/Customer/Payments/ProcessPayment | Process payment |
| GET | /api/v1/Customer/Payments/successPayment?orderid={value} | Payment success callback |

### Cashier Area

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| POST | /api/v1/Cashier/Orders/AddOrder | Create order by cashier |
| PATCH | /api/v1/Cashier/Orders/SubmitOrder | Submit order |
| DELETE | /api/v1/Cashier/Orders/CancelOrder | Cancel current cashier order |
| DELETE | /api/v1/Cashier/Orders/CancelOrder/{id} | Cancel cashier order by id |
| PATCH | /api/v1/Cashier/Orders/UpdateOrder | Update cashier order |
| POST | /api/v1/Cashier/OrderItems/AddItemToOrder | Add item to order |
| DELETE | /api/v1/Cashier/OrderItems/DeleteItemFromOrder/{id} | Delete item from order |
| GET | /api/v1/Cashier/OrderItems/ViewOrders | View cashier orders |
| GET | /api/v1/Cashier/OrderItems/getOrderDetailsById/{id} | View order details by id |
| PATCH | /api/v1/Cashier/OrderItems/UpdateQuantity | Update order item quantity |
| GET | /api/v1/Cashier/OrderItems/getOrderReport/{id} | Get order report by id |

### Chef Area

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/Chef/Orders/viewOrders | View chef orders |
| PATCH | /api/v1/Chef/Orders/ChangeStatus/{id} | Change order status by id |

### Delivery Area

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/Delivery/Orders/ViewOrders | View delivery orders |
| GET | /api/v1/Delivery/Orders/getOrderDetails/{id} | View order details by id |
| PATCH | /api/v1/Delivery/Orders/ChangeStatusToOutForDelivery/{id} | Mark order as out for delivery |
| GET | /api/v1/Delivery/Orders/viewOutForDeliveryOrders | View out-for-delivery orders |
| PATCH | /api/v1/Delivery/Orders/ChangeStatusToDelivered/{id} | Mark order as delivered |

### Manager Area

| HTTP Method | Endpoint | Description |
| ----------- | -------- | ----------- |
| GET | /api/v1/Manager/Categories/GetAll | Get all categories (manager) |
| GET | /api/v1/Manager/Categories/GetById/{id} | Get category by id (manager) |
| POST | /api/v1/Manager/Categories/Add | Add category |
| PUT | /api/v1/Manager/Categories/Update | Update category |
| DELETE | /api/v1/Manager/Categories/Delete/{id} | Delete category |
| POST | /api/v1/Manager/MenuItems/Add | Add menu item |
| DELETE | /api/v1/Manager/MenuItems/Delete/{id} | Delete menu item |
| PUT | /api/v1/Manager/MenuItems/Update | Update menu item |
| GET | /api/v1/MenuItems/GetAllMenuItems | Get all menu items |
| GET | /api/v1/Manager/MenuItems/GetById/{id} | Get menu item by id |
| PATCH | /api/v1/ManagerOrChef/MenuItems/toggleStatus/{id} | Toggle menu item status |
| POST | /api/v1/Manager/Menus/Add | Add menu |
| DELETE | /api/v1/Manager/Menus/Delete/{id} | Delete menu |
| PUT | /api/v1/Manager/Menus/Update | Update menu |

---

## 🗂 Folder Structure

```text
src/
|-- Luqma.API/
|   |-- Areas/
|   |   |-- Cashier/Controllers/
|   |   |-- Chef/controllers/
|   |   |-- Customer/Controllers/
|   |   |-- Delivery/Controllers/
|   |   `-- Manager/Controllers/
|   |-- Base/
|   |-- Controllers/
|   |-- Program.cs
|   `-- appsettings*.json
|-- Luqma.Core/
|   |-- Bases/
|   |-- Behaviors/
|   |-- Features/
|   |-- GlobalFunctions/
|   |-- Middlewares/
|   `-- Resources/
|-- Luqma.Data/
|   |-- Entities/
|   |-- Enum/
|   |-- Routing/Router.cs
|   `-- Response/
|-- Luqma.Infrastructure/
|   |-- Data/
|   |-- Configurations/
|   |-- Repositories/
|   `-- Migrations/
`-- Luqma.Service/
    |-- Abstractions/
    |-- Implementations/
    `-- DependencyInjection/

ERD/
`-- Luqma.svg
```

| Project | Responsibility |
| ------- | -------------- |
| `Luqma.API` | Exposes HTTP endpoints, auth, area-based controllers, and API setup |
| `Luqma.Core` | Application use-cases (CQRS), pipeline behaviors, validation, and business orchestration |
| `Luqma.Data` | Entities, DTO/response contracts, enums, and centralized route constants |
| `Luqma.Infrastructure` | EF Core context, repositories, persistence config, and migrations |
| `Luqma.Service` | External/service integrations (mail, media, notifications, helpers) |

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
