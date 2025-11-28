namespace Luqma.Service.Interfaces
{
    public interface IEmailService
    {
        public Task<string> SendAuthenticationsEmailAsync(string email, string linkOrCode, string subject, string name);
        public  Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
