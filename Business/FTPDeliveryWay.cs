using IBusiness;
using static IBusiness.Enums;

namespace Business
{
    public class FTPDeliveryWay : IDeliveryWay
    {
        private readonly DeliverWays _deliverWays = DeliverWays.FTP;
        public void Deliver(string toEmail, string filePath)
        {
            //throw new NotImplementedException();
        }

        public bool IsMatch(DeliverWays deliverWay)
        {
            return deliverWay == _deliverWays;
        }
    }
}
