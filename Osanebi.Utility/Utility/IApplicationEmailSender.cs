using System.Net.Mail;

namespace Osanebi.Utility.Utility
{
    public interface IApplicationEmailSender
    {
        Task SendEmailAsync(MailMessage message);
    }
}
