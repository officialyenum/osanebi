using Microsoft.Extensions.Options;
using Osanebi.Utility.Utility;
using System.Net.Mail;

namespace Osanebi.Utility
{
    public class ApplicationEmailSender(IOptions<EmailConfiguration> emailConfiguration) : IApplicationEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration = emailConfiguration.Value;

        public Task SendEmailAsync(MailMessage message)
        {
            message.From = new MailAddress(_emailConfiguration.FromEmail, _emailConfiguration.FromName);
            SmtpClient smtpClient = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.Port)
            {
                Credentials = new System.Net.NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password),
                EnableSsl = true
            };
            return smtpClient.SendMailAsync(message);
        }
    }
}
