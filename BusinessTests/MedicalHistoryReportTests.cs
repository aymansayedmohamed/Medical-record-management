using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Collections.Generic;
using System.IO;

namespace BusinessTests
{
    [TestClass]
    public class MedicalHistoryReportTests
    {

        [TestMethod]
        public void RenderReport_WithPassedData_ReturnFilePath()
        {
            List<PatientMedicalHistory> medicalHistory = new List<PatientMedicalHistory>(){ new PatientMedicalHistory()
            {
                PatientName = "Ayman",
                SocialNumber = "123456788",
                Doctor = new Doctor() { ID = "1", Name = "d1" },
                Email = "amohamed@integrant.com",
                Hospital = new Hospital() { ID = "1", Name = "h1" },
                Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
            }};


            MedicalHistoryReport medicalHistoryReport = new MedicalHistoryReport();

            string filePath = medicalHistoryReport.RenderReport(medicalHistory);


            Assert.IsTrue(File.Exists(filePath));

            File.Delete(filePath);

        }

        [TestMethod]
        public void RenderReport_WithNullData_returnNull()
        {

            MedicalHistoryReport medicalHistoryReport = new MedicalHistoryReport();

            string filePath = medicalHistoryReport.RenderReport(null);


            Assert.IsNull(filePath);

        }


        [TestMethod]
        public void RenderReport_TheSamePatientDataMoreThanOne_OverwriteExistingFile()
        {
            List<PatientMedicalHistory> medicalHistory = new List<PatientMedicalHistory>(){ new PatientMedicalHistory()
            {
                PatientName = "Ayman",
                SocialNumber = "123456788",
                Doctor = new Doctor() { ID = "1", Name = "d1" },
                Email = "amohamed@integrant.com",
                Hospital = new Hospital() { ID = "1", Name = "h1" },
                Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
            }};


            MedicalHistoryReport medicalHistoryReport = new MedicalHistoryReport();


            string filePath = medicalHistoryReport.RenderReport(medicalHistory);

            Assert.IsTrue(File.Exists(filePath));


            filePath = medicalHistoryReport.RenderReport(medicalHistory);

            Assert.IsTrue(File.Exists(filePath));

            File.Delete(filePath);
        }


        [TestMethod]
        public void generateReportHTML_WithData_ReturnHtmlString()
        {
            List<PatientMedicalHistory> medicalHistory = new List<PatientMedicalHistory>(){ new PatientMedicalHistory()
            {
                PatientName = "Ayman",
                SocialNumber = "123456788",
                Doctor = new Doctor() { ID = "1", Name = "d1" },
                Email = "amohamed@integrant.com",
                Hospital = new Hospital() { ID = "1", Name = "h1" },
                Medicines = new List<Medicine>() { new Medicine() { ID = "1", Name = "M1" }, new Medicine() { ID = "2", Name = "M2" } },
                Surgeries = new List<Surgery>() { new Surgery() { ID = "1", Name = "S1" }, new Surgery() { ID = "2", Name = "S2" } }
            }};

            PrivateObject privateObjectMedicalHistoryReport = new PrivateObject(typeof(MedicalHistoryReport));

            string htmlString = (string)privateObjectMedicalHistoryReport.Invoke("generateReportHTML", (new object[1] { medicalHistory }));


            Assert.IsTrue(!string.IsNullOrEmpty(htmlString));

        }

        [TestMethod]
        public void generateReportHTML_WithNullData_ReturnNull()
        {

            PrivateObject privateObjectMedicalHistoryReport = new PrivateObject(typeof(MedicalHistoryReport));

            var htmlString = (string)privateObjectMedicalHistoryReport.Invoke("generateReportHTML", (new object[1] { null }));


            Assert.IsTrue(string.IsNullOrEmpty(htmlString));

        }



    }
}
