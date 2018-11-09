using System.Net;
using System.Net.Mail;

namespace IBusiness
{
    public interface ISmtpClient
    {
        int Port { get; set; }
        NetworkCredential Credentials { get; set; }
        bool EnableSsl { get; set; }
        string SmtpClient { get; set; }
        MailMessage MailMessage { get; set; }

        void  Send();
    }
}
