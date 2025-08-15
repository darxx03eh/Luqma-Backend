namespace Luqma.Service.Interfaces
{
    public interface IWhatsAppService
    {
        public Task<string> SendPhoneNumberConfirmationCodeAsync(string receiver, string code);
    }
}
