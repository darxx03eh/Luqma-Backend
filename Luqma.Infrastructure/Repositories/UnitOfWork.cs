using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<LuqmaUser> userManager;

        public UnitOfWork(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<LuqmaUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            RefreshTokenRepository = new RefreshTokenRepository(context, httpContextAccessor);
            UserRepository = new UserRepository(context, httpContextAccessor, userManager);
            UserAddressRepository = new UserAddressRepository(context, httpContextAccessor);
            DeductionRepository = new DeductionRepository(context, httpContextAccessor, userManager);
            SalaryRepository = new SalaryRepository(context, httpContextAccessor, userManager);
            BillRepository = new BillRepository(context, httpContextAccessor);
            KitchenItemsRepository = new KitchenItemsRepository(context, httpContextAccessor);
            KitchenRequirmentsRepository = new KitchenRequirmentsRepository(context, httpContextAccessor);
            RequirmentItemsRepository = new RequirmentItemsRepository(context, httpContextAccessor);
            FeedbackRepository = new FeedbackRepository(context, httpContextAccessor);
            CustomerRepository = new CustomerRepository(context, httpContextAccessor);
            OrderRepository = new OrderRepository(context, httpContextAccessor);
            MenuItemRepository = new MenuItemRepository(context, httpContextAccessor);
        }

        public IUserRepository UserRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
        public IUserAddressRepository UserAddressRepository { get; set; }
        public IDeductionRepository DeductionRepository { get; set; }
        public ISalaryRepository SalaryRepository { get; set; }
        public IBillRepository BillRepository { get; set; }
        public IKitchenItemsRepository KitchenItemsRepository { get; set; }
        public IKitchenRequirmentsRepository KitchenRequirmentsRepository { get; set; }
        public IRequirmentItemsRepository RequirmentItemsRepository { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IMenuItemRepository MenuItemRepository { get; set; }
    }
}
