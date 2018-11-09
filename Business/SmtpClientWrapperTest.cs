using IBusiness;
using System.Net;
using System.Net.Mail;

namespace Business
{
    public class SmtpClientWrapperTest : ISmtpClient
    {
        public int Port { get; set; }
        public NetworkCredential Credentials { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpClient { get; set; }
        public MailMessage MailMessage { get  ; set ; }

        public void Send()
        {
        }


    }
}
