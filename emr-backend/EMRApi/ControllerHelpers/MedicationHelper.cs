using BusinessLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Models;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace emr_backend.ControllerHelpers
{

    public class MedicationHelper
    {

        #region Converters

        /// <summary>
        /// Converts database models to a business logic object.
        /// </summary>
        public static Medication Convert(Medication_db instance)
        {
            if (instance == null)
                return null;

            return new Medication(new(instance.Name),
                instance.SideEffects, instance.Treats
                );
        }

        #endregion

        public static ResponseMessage Get(string name,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var dbInstance = MedicationHelper_db.Get(name, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the medication";

            var response = new ResponseMessage
                (
                    dbInstance != null,
                    statusResponse.Message,
                    Convert(dbInstance)
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage Add(JsonElement data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var rowsAffected = MedicationHelper_db.Add(data, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding medication";

            var response = new ResponseMessage
                (
                    rowsAffected != 0,
                    statusResponse.Message
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetCollection(DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var medList = MedicationHelper_db.GetCollection(context, out StatusResponse statusResponse);

            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong when retreiving medication name list";

            var response = new ResponseMessage
                (
                    medList != null,
                    statusResponse.Message,
                    medList
                );

            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage GetWhatTreats(string symptom, DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var symptomList = MedicationHelper_db.GetWhatTreats(symptom, context, out StatusResponse statusResponse);

            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong when retreiving patient ssn list";

            var response = new ResponseMessage
                (
                    symptomList != null,
                    statusResponse.Message,
                    symptomList
                );

            statusCode = statusResponse.StatusCode;
            return response;
        }
    }
}
