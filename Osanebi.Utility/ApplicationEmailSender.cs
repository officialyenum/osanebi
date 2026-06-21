using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Osanebi.Utility.Utility;

namespace Osanebi.Utility
{
    public class ApplicationEmailSender(IOptions<EmailConfiguration> emailConfiguration) : IApplicationEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration = emailConfiguration.Value;

        public async Task SendEmailAsync(MimeMessage message)
        {
            message.From.Add(
                new MailboxAddress(_emailConfiguration.FromName, _emailConfiguration.FromEmail));
            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
            await client.AuthenticateAsync(
                    _emailConfiguration.Username,
                    _emailConfiguration.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}
