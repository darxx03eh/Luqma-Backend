namespace Luqma.Service.Interfaces
{
    public interface IWhatsAppService
    {
        public Task<string> SendPhoneNumberConfirmationCodeAsync(string receiver, string code);
        public Task<string> SendOrderIdForCustomerAsync(string receiver, int OrderId);
    }
}
