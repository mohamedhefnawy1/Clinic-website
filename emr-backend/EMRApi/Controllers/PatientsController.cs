using BusinessLibrary.Models;
using emr_backend.ContextHelpers;
using emr_backend.ControllerHelpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace emr_backend.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : Controller
    {
        #region Initialization

        /// <summary>
        /// Reference to the hosting environment instance added in the Startup.cs.
        /// </summary>
        private readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// Reference to the database context helper instance added in the Startup.cs.
        /// </summary>
        private readonly DatabaseContextHelper DbContextHelper;

        /// <summary>
        /// Constructor called by the service provider.
        /// Using injection to get the arguments.
        /// </summary>
        public PatientsController(IHostingEnvironment hostingEnvironment, DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            DbContextHelper = database;
        }

        #endregion

        
        [HttpGet("{ssn}")]
        public ResponseMessage GetPatient(int ssn)
        {
            var response = PatientHelper.Get(
                ssn: ssn,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int) statusCode;
            return response;
        }

        [HttpPost("")]
        public ResponseMessage AddPatient([FromBody] JsonElement data)
        {
            var response = PatientHelper.Add(
                data: data,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpPost("{ssn}/EMR")]
        public ResponseMessage AddDiagnosis(int ssn, [FromBody] JsonElement data)
        {
            var response = PatientHelper.AddDiagnosis(
                ssn: ssn,
                data: data,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpGet("")]
        public ResponseMessage GetPatients()
        {
            var response = PatientHelper.GetCollection(
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
