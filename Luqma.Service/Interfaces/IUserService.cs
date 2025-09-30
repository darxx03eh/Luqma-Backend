namespace Luqma.Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CheckPasswordAsync(string password);
        public Task<string> ChangePasswordAsync(string password, string newPasswordConfirm);
        public Task<string> ChangeNameAsync(string firstName, string lastName);
    }
}
