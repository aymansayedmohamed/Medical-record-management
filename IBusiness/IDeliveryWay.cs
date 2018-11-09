using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBusiness.Enums;

namespace IBusiness
{
    public interface IDeliveryWay
    {
        bool IsMatch(DeliverWays wayName);
        void Deliver(string ToEmail,string FilePath);
    }
}
