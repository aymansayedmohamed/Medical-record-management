using static IBusiness.Enums;

namespace IBusiness.IVaildators
{
    public interface IPatientMedicalHistoryServiceValidator
    {
        void ValidateGet(string SocialNumber);
        void ValidateDeliver(string SocialNumber, DeliverWays DeliverWay);

    }
}