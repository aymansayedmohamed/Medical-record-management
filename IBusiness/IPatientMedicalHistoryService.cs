using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBusiness.Enums;

namespace IBusiness
{
    public interface IPatientMedicalHistoryService
    {
         IQueryable<PatientMedicalHistory> Get(string socialNumber);
         void Deliver(string SocialNumber, DeliverWays deliverWay);
    }
}
