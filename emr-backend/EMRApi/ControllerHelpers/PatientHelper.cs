using BusinessLibrary.Models;
using DatabaseLibrary.Core;
using DatabaseLibrary.Helpers;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace emr_backend.ControllerHelpers
{
    public class PatientHelper
    {
        public static Patient Convert(Patient_db instance)
        {
            if (instance == null)
                return null;

            List<object> newRecord = new();
            foreach (var (symptomDB, diagnosisDB, prescriptionDB) in instance.Emr.Record)
            {
                (Symptom symptom, Diagnosis diagnosis, Prescription prescription) = (
                    new Symptom(symptomDB.S_description, symptomDB.Body_area, int.Parse(symptomDB.Observer_ssn)),
                    new Diagnosis(diagnosisDB.Illness, new Date(int.Parse(diagnosisDB.DiagnosisDate.Year), int.Parse(diagnosisDB.DiagnosisDate.Month), int.Parse(diagnosisDB.DiagnosisDate.Day))),
                    new Prescription(prescriptionDB.M_name, prescriptionDB.Dosage)
                    );

                newRecord.Add(new { symptom, diagnosis, prescription });
            }

            EMR emr = new(int.Parse(instance.SSN), new Guid(instance.Emr.Emr_id), newRecord);
            return new Patient
                (
                int.Parse(instance.SSN), new Name(instance.Name.FirstName, instance.Name.LastName),
                instance.PhoneNumber, instance.Address, instance.Sex,
                new Date(int.Parse(instance.DateOfBirth.Year), int.Parse(instance.DateOfBirth.Month), int.Parse(instance.DateOfBirth.Day)),
                emr
                );
        }

        public static ResponseMessage Get(int ssn,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var dbInstance = PatientHelper_db.Get(ssn, context, out StatusResponse statusResponse);


            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while retrieving the patient";

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
            var rowsAffected = PatientHelper_db.Add(data, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding patient";

            var response = new ResponseMessage
                (
                    rowsAffected != 0,
                    statusResponse.Message
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }

        public static ResponseMessage AddDiagnosis(int ssn, JsonElement data,
            DbContext context, out HttpStatusCode statusCode, bool includeDetailedErrors = false)
        {
            var rowsAffected = PatientHelper_db.AddDiagnosis(ssn, data, context, out StatusResponse statusResponse);

            // Get rid of detailed error message (when requested)
            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError
                && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong while adding diagnosis";

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
            var dbList = PatientHelper_db.GetCollection(context, out StatusResponse statusResponse);

            if (statusResponse.StatusCode == HttpStatusCode.InternalServerError && !includeDetailedErrors)
                statusResponse.Message = "Something went wrong when retreiving patient ssn list";

            var response = new ResponseMessage
                (
                    dbList != null,
                    statusResponse.Message,
                    dbList
                );
            statusCode = statusResponse.StatusCode;
            return response;
        }
    }
}
