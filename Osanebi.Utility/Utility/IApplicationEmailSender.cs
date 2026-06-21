using MimeKit;

namespace Osanebi.Utility.Utility
{
    public interface IApplicationEmailSender
    {
        Task SendEmailAsync(MimeMessage message);
    }
}
