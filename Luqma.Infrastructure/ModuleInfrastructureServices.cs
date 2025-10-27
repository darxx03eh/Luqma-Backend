using Luqma.Data.Entities.Identity;
using Luqma.Data.Helpers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Luqma.Infrastructure
{
    public static class ModuleInfrastructureServices
    {
        public static IServiceCollection AddModuleInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Identity Settings
            services.AddIdentity<LuqmaUser, LuqmaRole>(options =>
            {
                // Sign in settings
                options.SignIn.RequireConfirmedEmail = true;
                // password settings
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                // lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
            }).AddEntityFrameworkStores<LuqmaDbContext>().AddDefaultTokenProviders();
            #endregion

            #region Prepare Settings
            var emailSettings = new EmailSettings();
            var cloudinarySettings = new CloudinarySettings();
            var jwtSettings = new JwtSettings();
            var encryptionSettings = new EncryptionSettings();
            var whatsAppSettings = new WhatsAppSettings();
            var OpenWeather = new OpenWeatherSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            configuration.GetSection(nameof(cloudinarySettings)).Bind(cloudinarySettings);
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            configuration.GetSection(nameof(encryptionSettings)).Bind(encryptionSettings);
            configuration.GetSection(nameof(whatsAppSettings)).Bind(whatsAppSettings);
            configuration.GetSection("OpenWeather").Bind(OpenWeather);

            services.AddSingleton(emailSettings);
            services.AddSingleton(cloudinarySettings);
            services.AddSingleton(jwtSettings);
            services.AddSingleton(encryptionSettings);
            services.AddSingleton(whatsAppSettings);
            services.AddSingleton(OpenWeather);

            #endregion

            #region Authentication and JWT Settings
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    RoleClaimType = nameof(UserClaimModel.Role),
                    NameClaimType = nameof(UserClaimModel.UserName),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            #region Dependancy injection
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IRefreshTokenRepository), typeof(RefreshTokenRepository));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddTransient(typeof(IMenuItemRepository), typeof(MenuItemRepository));
            services.AddTransient(typeof(IMenuContainsRepository), typeof(MenuContainsRepository));
            services.AddTransient(typeof(ICategoryItemRepository), typeof(CategoryItemRepository));
            services.AddTransient(typeof(IMenuRepository), typeof(MenuRepository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IDeductionRepository), typeof(DeductionRepository));
            services.AddTransient(typeof(ISalaryRepository), typeof(SalaryRepository));
            services.AddTransient(typeof(IUserAddressRepository), typeof(UserAddressRepository));
            services.AddTransient(typeof(ICategoryItemRepository), typeof(CategoryItemRepository));
            services.AddTransient(typeof(IMenuContainsRepository), typeof(MenuContainsRepository));
            services.AddTransient(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddTransient(typeof(ICartRepository), typeof(CartRepository));
            services.AddTransient(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient(typeof(IPaymentRepository), typeof(PaymentRepository));
            #endregion

            return services;
        }
    }
}
