using IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static IBusiness.Enums;

namespace APIS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MedicalHistoryController : ApiController
    {
        private readonly IPatientMedicalHistoryService _patientMedicalHistoryService;

        public MedicalHistoryController(IPatientMedicalHistoryService patientMedicalHistoryService)
        {
            _patientMedicalHistoryService = patientMedicalHistoryService;
        }


        [HttpGet]
        public HttpResponseMessage Search(string socialNumber)
        {
            try
            {
                List<Models.PatientMedicalHistory> PatientMedicalHistory = _patientMedicalHistoryService.Get(socialNumber).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, PatientMedicalHistory);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        public HttpResponseMessage DeliverByMail(string socialNumber)
        {
            try
            {
                _patientMedicalHistoryService.Deliver(socialNumber, DeliverWays.Email);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
