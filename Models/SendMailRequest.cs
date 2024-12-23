using MailSend.Enums;

namespace MailSend.Models
{
    public class SendMailRequest
    {
        public string?  Name { get; set; }
        public string? Otp {  get; set; }

        public string? Email { get; set; }
        public EmailTypes EmailType { get; set; }
        
    }
}
