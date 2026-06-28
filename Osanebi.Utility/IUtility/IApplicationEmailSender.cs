using MimeKit;

namespace Osanebi.Utility.IUtility
{
    public interface IApplicationEmailSender
    {
        Task SendEmailAsync(MimeMessage message);
    }
}
