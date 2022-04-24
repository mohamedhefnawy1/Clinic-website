using BusinessLibrary.Models;
using emr_backend.ContextHelpers;
using emr_backend.ControllerHelpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace emr_backend.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : Controller
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
        public DoctorsController(IHostingEnvironment hostingEnvironment, DatabaseContextHelper database)
        {
            HostingEnvironment = hostingEnvironment;
            DbContextHelper = database;
        }

        #endregion

        [HttpGet("{ssn}")]
        public ResponseMessage GetDoctor(int ssn)
        {
            var response = DoctorHelper.Get(
                ssn: ssn,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpGet("")]
        public ResponseMessage GetDoctors()
        {
            var response = DoctorHelper.GetCollection(
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }

        [HttpPost("")]
        public ResponseMessage AddDoctor([FromBody] JsonElement data)
        {
            var response = DoctorHelper.Add(
                data: data,
                context: DbContextHelper.DbContext,
                statusCode: out HttpStatusCode statusCode,
                includeDetailedErrors: HostingEnvironment.IsDevelopment());
            HttpContext.Response.StatusCode = (int)statusCode;
            return response;
        }
    }
}
