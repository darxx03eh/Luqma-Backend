using Luqma.Data.Helpers;
using Luqma.Service.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Luqma.Service.Implementations
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly WhatsAppSettings whatsAppSettings;

        public WhatsAppService(WhatsAppSettings whatsAppSettings)
        {
            this.whatsAppSettings = whatsAppSettings;
        }

        public async Task<string> SendPhoneNumberConfirmationCodeAsync(string receiver, string code)
        {
            try
            {
                TwilioClient.Init(whatsAppSettings.AccountSid, whatsAppSettings.AuthToken);
                var message = MessageResource.Create(
                    from: new PhoneNumber($"whatsapp:{whatsAppSettings.FromNumber}"),
                    to: new PhoneNumber($"whatsapp:{receiver}"),
                    body:
                        "*🍽️ LUQMA RESTAURANT*\n" +
                        $"━━━━━━━━━━━━\n\n" +
                        $"*Verification Code:* `{code}`\n" +
                        "⏰ *Expires in:* 10 minutes\n" +
                        "Please enter this code to verify your phone number and start your culinary journey with us.\n" +
                        "*Welcome aboard!* 🍽️"
                    );
                return "Success";
            }
            catch(Exception exp)
            {
                return "Failed";
            }
        }
        public async Task<string> SendOrderIdForCustomerAsync(string receiver, int OrderId)
        {
            try
            {
                TwilioClient.Init(whatsAppSettings.AccountSid, whatsAppSettings.AuthToken);
                var message = MessageResource.Create(
                    from: new PhoneNumber($"whatsapp:{whatsAppSettings.FromNumber}"),
                    to: new PhoneNumber($"whatsapp:{receiver}"),
                    body:
                        "*🍽️ LUQMA RESTAURANT*\n" +
                        $"━━━━━━━━━━━━\n\n" +
                        $"*Your Order Id:* `{OrderId}`\n"
                       
                    );
                return "Success";
            }
            catch (Exception exp)
            {
                return "Failed";
            }
        }
    }
}
