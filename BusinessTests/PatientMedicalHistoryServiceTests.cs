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
using Models;
using IDataAccessLayer;
using DataAccessLayer;
using Business.Vaildators;

namespace BusinessTests
{
    [TestClass]
    public class PatientMedicalHistoryServiceTests
    {

       
        [TestMethod]
        public void Get_ExistingPatientData_ReturnData()
        {
            IRepository<PatientMedicalHistory> _PatientMedicalHistory = new Repository<PatientMedicalHistory>();
            for (int i = 0; i < 10; i++)
            {
                var PatientMedicalHistory = new PatientMedicalHistory()
                {
                    PatientName = "Ayman",
                    SocialNumber = "123456789",
                    Doctor = new Doctor() { ID = i.ToString(), Name = $"d{i}" },
                    Email = "amohamed@integrant.com",
                    Hospital = new Hospital() { ID = $"{i}", Name = $"h{i}" },
                    Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                    Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
                };

                _PatientMedicalHistory.Add(PatientMedicalHistory);
            }

            var patientMedicalHistoryService = new PatientMedicalHistoryService(new UnitOfWork(new Repository<PatientMedicalHistory>())
                ,new List<IDeliveryWay>() { new EmailDeliveryWay(new Email(new SmtpClientWrapperTest()))},new MedicalHistoryReport(),new PatientMedicalHistoryServiceValidator());


            var data = patientMedicalHistoryService.Get("123456789");

            Assert.IsTrue(data.Any());


        }



        [TestMethod]
        public void Get_NotExistingPatientData_NotReturnData()
        {
           
            var patientMedicalHistoryService = new PatientMedicalHistoryService(new UnitOfWork(new Repository<PatientMedicalHistory>())
                , new List<IDeliveryWay>() { new EmailDeliveryWay(new Email(new SmtpClientWrapperTest())) }, new MedicalHistoryReport(),new PatientMedicalHistoryServiceValidator());


            var data = patientMedicalHistoryService.Get("");

            Assert.IsTrue(!data.Any());


        }

        [TestMethod]
        public void Deliver_ExistingPatientData_SendMail()
        {
            IRepository<PatientMedicalHistory> _PatientMedicalHistory = new Repository<PatientMedicalHistory>();
            for (int i = 0; i < 10; i++)
            {
                var PatientMedicalHistory = new PatientMedicalHistory()
                {
                    PatientName = "Ayman",
                    SocialNumber = "123456789",
                    Doctor = new Doctor() { ID = i.ToString(), Name = $"d{i}" },
                    Email = "amohamed@integrant.com",
                    Hospital = new Hospital() { ID = $"{i}", Name = $"h{i}" },
                    Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                    Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
                };

                _PatientMedicalHistory.Add(PatientMedicalHistory);
            }

            var patientMedicalHistoryService = new PatientMedicalHistoryService(new UnitOfWork(new Repository<PatientMedicalHistory>())
                , new List<IDeliveryWay>() { new EmailDeliveryWay(new Email(new SmtpClientWrapperTest())) }, new MedicalHistoryReport(),new PatientMedicalHistoryServiceValidator());


            patientMedicalHistoryService.Deliver("123456789",Enums.DeliverWays.Email);



        }



        [TestMethod]
        public void Deliver_NotExistingPatientData_ThrowException()
        {
            var patientMedicalHistoryService = new PatientMedicalHistoryService(new UnitOfWork(new Repository<PatientMedicalHistory>())
                , new List<IDeliveryWay>() { new EmailDeliveryWay(new Email(new SmtpClientWrapperTest())) }, new MedicalHistoryReport(), new PatientMedicalHistoryServiceValidator());


            ;

            var ex = Assert.ThrowsException<Exception>(() => patientMedicalHistoryService.Deliver("", Enums.DeliverWays.Email));

            Assert.AreEqual(ex.Message, "No data foun for this user");


        }
    }
}
