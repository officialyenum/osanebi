namespace Osanebi.Utility
{
    public class EmailConfiguration
    {
        public required string FromName { get; set; }
        public required string FromEmail { get; set; }
        public required string SmtpServer { get; set; }
        public int Port { get; set; } = 587;
        public string Security { get; set; } = "SSL";
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
