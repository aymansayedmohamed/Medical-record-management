using IBusiness;
using System.Net;
using System.Net.Mail;

namespace Business
{
    public class SmtpClientWrapper : ISmtpClient
    {
        public int Port { get; set; }
        public NetworkCredential Credentials { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpClient { get; set; }
        public MailMessage MailMessage { get  ; set ; }

        public void Send()
        {
            using (SmtpClient SmtpClient = new SmtpClient(this.SmtpClient))
            {

                SmtpClient.Port = Port;

                SmtpClient.Credentials = Credentials;

                SmtpClient.EnableSsl = EnableSsl;

                SmtpClient.Send(MailMessage);
            }
        }


    }
}
