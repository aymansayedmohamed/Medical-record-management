using IBusiness;
using System;
using static IBusiness.Enums;

namespace Business
{
    public class EmailDeliveryWay : IDeliveryWay
    {
        private readonly DeliverWays __deliverWays = DeliverWays.Email;
        private IEmail _email;

        public EmailDeliveryWay(IEmail email)
        {
            _email = email;
        }

        public void Deliver(string toEmail, string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                    _email.Send(toEmail, "Medical History Report", "Kindly find your medical history report at the attachments", filePath);
                else
                    throw new Exception("File Not Found");
            }
            catch (Exception)
            {
                //log the error  then

                throw;
            }
        }

        public bool IsMatch(DeliverWays deliverWay)
        {
            return deliverWay == __deliverWays;
        }
    }
}
