using BusinessLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Models;
using System.Net;
using System.Text.Json;

namespace emr_backend.ControllerHelpers
{
    public class DoctorHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static Doctor Convert(Doctor_db instance)
        {
            if (instance == null)
                return null;
            return new Doctor(int.Parse(instance.SSN), new Name(instance.Name.FirstName, instance.Name.LastName),
                instance.PhoneNumber, instance.Address, instance.Sex,
                new Date(int.Parse(instance.DateOfBirth.Year), int.Parse(instance.DateOfBirth.Month), int.Parse(instance.DateOfBirth.Day)),
                instance.Field,
                new EducationalBackground(instance.EducationalBackground.School, instance.EducationalBackground.Years),
                new Clinic(instance.Clinic.Location, instance.Clinic.Name)
                );
        }

        #endregion

        /// <summary>
        /// Gets a single doctor.
        /// </summary>
        /// <param name="includeDetailedErrors">States whether the internal server error message should be detailed or not.</param>
        public static ResponseMessage Get(int ssn,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var dbInstance = DoctorHelper_db.Get(ssn, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the doctor";

            var response = new ResponseMessage
                (
                    dbInstance != null,
                    statusResponse.Message,
                    Convert(dbInstance)
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetCollection(DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var dbList = DoctorHelper_db.GetCollection(context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving doctor list";

            var response = new ResponseMessage
                (
                    dbList != null,
                    statusResponse.Message,
                    dbList
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Add(JsonElement data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var rowsAffected = DoctorHelper_db.Add(data, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding doctor";

            var response = new ResponseMessage
                (
                    rowsAffected != 0,
                    statusResponse.Message
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }
    }
}
