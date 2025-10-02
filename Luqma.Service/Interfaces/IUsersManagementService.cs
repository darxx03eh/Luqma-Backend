namespace Luqma.Service.Interfaces
{
    public interface IUsersManagementService
    {
        public Task<string> DeActivateAsync(int id);
        public Task<string> ActivateAsync(int id);
    }
}
