using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using Luqma.Service.Implementations;
using Luqma.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Luqma.Service
{
    public static class ModuleServiceServices
    {
        public static IServiceCollection AddModuleServiceServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddTransient(typeof(ITokenService), typeof(TokenService));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
            services.AddTransient(typeof(IWhatsAppService), typeof(WhatsAppService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(ICloudinaryService), typeof(CloudinaryService));
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
            services.AddTransient(typeof(IUserAddressRepository), typeof(UserAddressRepository));
            services.AddTransient(typeof(IDeductionService), typeof(DeductionService));
            services.AddTransient(typeof(IMenuItemService), typeof(MenuItemService));
            services.AddTransient(typeof(IMenuService), typeof(MenuService));
            services.AddTransient(typeof(ISalaryService), typeof(SalaryService));
            return services;
        }
    }
}
