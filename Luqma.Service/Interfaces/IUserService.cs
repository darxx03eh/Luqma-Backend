namespace Luqma.Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> ChecPasswordAsync(string password);
        public Task<string> ChangePasswordAsync(string password, string newPasswordConfirm);
    }
}
