namespace Luqma.Infrastructure.IRepositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
        public IUserAddressRepository UserAddressRepository { get; set; }
        public IDeductionRepository DeductionRepository { get; set; }
        public ISalaryRepository SalaryRepository { get; set; }
    }
}
