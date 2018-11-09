using IBusiness;
using PdfSharp;
using PdfSharp.Pdf;
using System.Collections.Generic;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Business
{
    public class MedicalHistoryReport : IMedicalHistoryReport
    {
        public string RenderReport(List<Models.PatientMedicalHistory> medicalHistoryData)
        {
            if (medicalHistoryData?.Count > 0)
            {
                string mainDirectory = "D:\\Temp\\Reports";

                if (!System.IO.Directory.Exists(mainDirectory))
                {
                    System.IO.Directory.CreateDirectory(mainDirectory);
                }

                string filePath = $"{mainDirectory}\\{medicalHistoryData[0].SocialNumber}.pdf";

                string html = generateReportHTML(medicalHistoryData);

                PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.Letter);

                pdf.Save(filePath);

                return filePath;
            }

            return null;
        }

        private string generateReportHTML(List<Models.PatientMedicalHistory> medicalHistoryData)
        {
            if (medicalHistoryData?.Count > 0)
            {
                string reportHtml = "";

                reportHtml += "<table style=\"width: 100 %;border: 1px solid black; \">";
                reportHtml += "<thead>";
                reportHtml += " <tr>";
                reportHtml += " <th> Name </th >";
                reportHtml += " <th>Social Number</th>";
                reportHtml += "<th> Hospital </th >";
                reportHtml += " <th> Doctor </th >";
                reportHtml += " <th> Medicines </ th >";
                reportHtml += "<th> Surgeries </ th >";
                reportHtml += " </tr>";
                reportHtml += " </thead >";
                reportHtml += " <tbody >";

                foreach (Models.PatientMedicalHistory item in medicalHistoryData)
                {
                    reportHtml += "<tr>";
                    reportHtml += $"<td>{ item.PatientName}</td >";
                    reportHtml += $"<td>{ item.SocialNumber} </td >";
                    reportHtml += $"<td>{ item.Hospital.Name} </td >";
                    reportHtml += $"<td>{ item.Doctor.Name} </td >";

                    reportHtml += "<td>";
                    reportHtml += "<ul>";
                    foreach (Models.Medicine med in item.Medicines)
                    {
                        reportHtml += "<li>";
                        reportHtml += $"{ med.Name}";
                        reportHtml += "</li>";
                    }
                    reportHtml += "</ul>";
                    reportHtml += "</td>";

                    reportHtml += "<td>";
                    reportHtml += "<ul>";
                    foreach (Models.Surgery sur in item.Surgeries)
                    {
                        reportHtml += "<li>";
                        reportHtml += $"{ sur.Name}";
                        reportHtml += "</li>";
                    }
                    reportHtml += "</ul>";
                    reportHtml += "</td>";

                    reportHtml += "</tr>";
                }



                reportHtml += "</tbody>";
                reportHtml += "</table>";

                return reportHtml;
            }
            return null;
        }
    }


}
