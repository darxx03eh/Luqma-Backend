using Luqma.Data.Entities;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IUnitOfWork
    {
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
