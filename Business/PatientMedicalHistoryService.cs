using IBusiness;
using IBusiness.IVaildators;
using IDataAccessLayer;
using Models;
using System.Collections.Generic;
using System.Linq;
using static IBusiness.Enums;

namespace Business
{
    public class PatientMedicalHistoryService : IPatientMedicalHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly List<IDeliveryWay> _deliveryWays;

        private readonly IMedicalHistoryReport _medicalHistoryReport;

        private readonly IPatientMedicalHistoryServiceValidator _iPatientMedicalHistoryServiceValidator;
        

        public PatientMedicalHistoryService(IUnitOfWork unitOfWork, List<IDeliveryWay> meliveryWays, IMedicalHistoryReport medicalHistoryReport,
            IPatientMedicalHistoryServiceValidator iPatientMedicalHistoryServiceValidator)
        {
            _unitOfWork = unitOfWork;

            _deliveryWays = meliveryWays;

            _medicalHistoryReport = medicalHistoryReport;

            _iPatientMedicalHistoryServiceValidator = iPatientMedicalHistoryServiceValidator;
        }

        public IQueryable<Models.PatientMedicalHistory> Get(string socialNumber)
        {
            _iPatientMedicalHistoryServiceValidator.ValidateGet(socialNumber);

            return _unitOfWork.PatientMedicalHistoryRepository.Where(O => O.SocialNumber == socialNumber).AsQueryable<PatientMedicalHistory>();
        }

        public void Deliver(string socialNumber, DeliverWays deliverWay)
        {

            _iPatientMedicalHistoryServiceValidator.ValidateDeliver(socialNumber,deliverWay);

            IQueryable<PatientMedicalHistory> MedicalHistory = Get(socialNumber);

            if (MedicalHistory.Any())
            {
                List<PatientMedicalHistory> MedicalHistoryData = MedicalHistory.ToList();

                string filePath = _medicalHistoryReport.RenderReport(MedicalHistoryData);

                IDeliveryWay deliveryWay = _deliveryWays.Where(O => O.IsMatch(deliverWay)).SingleOrDefault();

                deliveryWay?.Deliver(MedicalHistoryData.FirstOrDefault().Email, filePath);
            }

            else
                throw new System.Exception("No data foun for this user");
        }

    }
}
