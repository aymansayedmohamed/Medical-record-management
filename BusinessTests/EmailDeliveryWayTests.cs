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
using BusinessTests.App_Start;

namespace BusinessTests
{
    [TestClass]
    public class EmailDeliveryWayTests
    {


        [TestInitialize]
        public void TestSetup()
        {
            NinjectWebCommon.Start();
        }


        [TestMethod]
        public void Deliver_WithExistFile_SendEmail()
        {
            var email = new EmailDeliveryWay(new Email(new SmtpClientWrapperTest()));

            string filePath = @"D:\Temp\Reports\123456789.pdf";

            email.Deliver("amohamed@integrant.com", filePath);
        }

        [TestMethod]
        public void Deliver_WithNullFilePath_ThrowExeption()
        {
            var email = new EmailDeliveryWay(new Email(new SmtpClientWrapperTest()));

            string filePath = null;

           var ex= Assert.ThrowsException<Exception>(() => email.Deliver("amohamed@integrant.com", filePath));

            Assert.AreEqual(ex.Message, "File Not Found");

        }
    }
}
