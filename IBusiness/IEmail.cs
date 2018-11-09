using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IEmail
    {
        void Send(string toMail, string subject, string body, string filePath);
    }
}
