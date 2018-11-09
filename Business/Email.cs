using Business.CustomException;
using IBusiness;
using System;
using System.Configuration;
using System.Net.Mail;

namespace Business
{
    public class Email : IEmail
    {

        private readonly string _smtpClient;
        private readonly string _mailFrom;
        private readonly int _mailPort;
        private readonly string _mailPassword;
        private readonly bool _enableSsl;
        ISmtpClient _smtpClientWrapper;

        public Email(ISmtpClient smtpClientWrapper)
        {
            _smtpClientWrapper = smtpClientWrapper;

            _smtpClient = GetConfigValue("SmtpClient");

            _mailFrom = GetConfigValue("MailFrom");

            string mailPort = GetConfigValue("MailPort");
            _mailPort = mailPort != null ? Convert.ToInt32(mailPort) : 0;

            _mailPassword = GetConfigValue("MailPassword");

            string enableSsl = GetConfigValue("EnableSsl");
            _enableSsl = enableSsl == "true" ? true : false;
        }

        private string GetConfigValue(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }

        public void Send(string toMail, string subject, string body, string filePath)
        {
            try
            {

                using (SmtpClient SmtpServer = new SmtpClient(_smtpClient))
                {
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(_mailFrom);

                    mail.To.Add(toMail);

                    mail.Subject = subject;

                    mail.Body = body;

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Attachment attachment = new Attachment(filePath);

                        mail.Attachments.Add(attachment);
                    }

                    _smtpClientWrapper.Port = _mailPort;

                    _smtpClientWrapper.Credentials = new System.Net.NetworkCredential(_mailFrom, _mailPassword);

                    _smtpClientWrapper.EnableSsl = _enableSsl;

                    _smtpClientWrapper.SmtpClient = _smtpClient;

                    _smtpClientWrapper.MailMessage = mail;

                    _smtpClientWrapper.Send();
                }
            }
            catch (Exception ex)
            {
                throw new EMailException();
            }
        }

    }
}
