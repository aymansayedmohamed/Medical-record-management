using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IBusiness;
using Moq;
using Business;

namespace BusinessTests
{
    [TestClass]
    public class EmailTests
    {



        [TestMethod]
        public void Send_WithFullData_SendEmail()
        {
            
            var email = new Email(new SmtpClientWrapperTest());


            string toMail = "amohamed@integrant.com";
            string subject = "Test Senting Mail from unit test";
            string body = "Test Senting Mail from unit test Body";
            string filePath = null;

            email.Send(toMail, subject, body, filePath);
        }
    }
}
