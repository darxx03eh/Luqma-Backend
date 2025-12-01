using Luqma.Data.Helpers;
using Luqma.Service.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Luqma.Service.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IHostEnvironment env;
        private readonly EmailSettings emailSettings;

        public EmailService(IHttpContextAccessor httpContextAccessor, IHostEnvironment env,
                            EmailSettings emailSettings)
        {
            this.env = env;
            this.emailSettings = emailSettings;
        }
        public async Task<string> SendAuthenticationsEmailAsync(string email, string linkOrCode, string subject, string name)
        {
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Luqma", emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress(name, email));
                    message.Subject = subject;
                    message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = await GetLocalizedEmailBodyAsync(linkOrCode)
                    };
                    await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.Port, emailSettings.UseSSL);
                    await client.AuthenticateAsync(emailSettings.FromEmail, emailSettings.Password);
                    await client.SendAsync(message);
                    return "Success";
                }
            }
            catch (Exception exp)
            {
                return "Failed";
            }
        }
        private async Task<string> GetLocalizedEmailBodyAsync(string linkOrCode)
        {
            string fileName = "";
            if (linkOrCode.Length > 6)
                fileName = "ConfirmationEmail.html";
            else fileName = "ForgetPassword.html";
            var filePath = Path.Combine(env.ContentRootPath, "Templates", fileName);
            var htmlContent = await System.IO.File.ReadAllTextAsync(filePath, Encoding.UTF8);
            htmlContent = htmlContent.Replace("{linkOrCode}", linkOrCode);
            htmlContent = htmlContent.Replace("{year}", DateTime.UtcNow.Year.ToString());
            return htmlContent;
        }
      
    }
}
