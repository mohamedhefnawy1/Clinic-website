using BusinessLibrary.Models;
using emr_backend.ContextHelpers;
using emr_backend.ControllerHelpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace emr_backend.Controllers
{
    [Route("api/medication")]
    [ApiController]
    public class MedicationsController : Controller
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
        public MedicationsController(IHostingEnvironment hostingEnvironment, DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            DbContextHelper = database;
        }

        #endregion

        [HttpGet("{name}")]
        public ResponseMessage GetMedication(string name)
        {
            var response = MedicationHelper.Get(
                name: name,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpPost("")]
        public ResponseMessage AddMedication([FromBody] JsonElement data)
        {
            var response = MedicationHelper.Add(
                data: data,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpGet("")]
        public ResponseMessage GetNames()
        {
            var response = MedicationHelper.GetCollection
                (
                    context: DbContextHelper.DbContext,
                    statusCode: out HttpStatusCode statusCode,
                    includeDetailedErrors: HostingEnvironment.IsDevelopment()
                );
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpGet("treats/{s_description}")]
        public ResponseMessage GetWhatTreats(string s_description)
        {
            var response = MedicationHelper.GetWhatTreats
                (
                symptom: s_description,
                context: DbContextHelper.DbContext,
                out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment()
                );
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
