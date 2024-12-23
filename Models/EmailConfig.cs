namespace MailSend.Models
{
    public class EmailConfig
    {
        public string FromName { get; set; }
        public string FromAddess { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
