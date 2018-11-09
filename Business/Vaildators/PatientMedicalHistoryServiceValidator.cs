using IBusiness;
using IBusiness.IVaildators;
using IDataAccessLayer;
using Models;
using System.Collections.Generic;
using System.Linq;
using static IBusiness.Enums;

namespace Business.Vaildators
{
    public class PatientMedicalHistoryServiceValidator : IPatientMedicalHistoryServiceValidator
    {
        public void ValidateGet(string socialNumber)
        {
            #region Validation

            if (socialNumber == null)
            {
                throw new System.Exception("InvalidSocialNumber");
            }

            #endregion

        }

        public void ValidateDeliver(string socialNumber, DeliverWays deliverWay)
        {
            #region Validation

            if (socialNumber == null)
            {
                throw new System.Exception("InvalidSocialNumber");
            }

            #endregion
        }
    }
}
