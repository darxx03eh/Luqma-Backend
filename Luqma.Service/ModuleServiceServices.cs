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
            services.AddTransient(typeof(ICategoryItemService), typeof(CategoryItemService));
            services.AddTransient(typeof(IMenuContainService), typeof(MenuContainService));
            services.AddTransient(typeof(IBillService), typeof(BillService));

          


           
            services.AddTransient(typeof(IKitchenItemsService), typeof(KitchenItemsService));
            services.AddTransient(typeof(IKitchenRequirmentsService), typeof(KitchenRequirmentsService));
          
           services.AddTransient(typeof(ICustomerService), typeof(CustomerService));
            services.AddTransient(typeof(ICartService), typeof(CartService));
            services.AddTransient(typeof(IWeatherService), typeof(WeatherService));
            services.AddTransient(typeof(IOrderService), typeof(OrderService));



            services.AddHttpClient<WeatherService>();
            return services;
        }
    }
}
