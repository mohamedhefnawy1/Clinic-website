using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;


namespace DatabaseLibrary.Helpers
{
    public class PatientHelper_db
    {
        public static Patient_db Get(int ssn, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                DataTable table = context.ExecuteDataQueryCommand
                    (
                    commandText:    "select  p.*, n.f_name, n.l_name, b.dob_month, b.dob_day, b.dob_year, per.phone_no, per.address, per.sex, e.emr_id\n" + 
                                    "from patient as p\n" + 
                                    "join p_name as n on n.ssn = p.ssn\n" + 
                                    "join dob as b on b.ssn = p.ssn\n" + 
                                    "join person as per on per.ssn = p.ssn\n" + 
                                    "join emr as e on e.p_ssn = p.ssn\n" +
                                    "where p.ssn = @ssn",

                        parameters: new Dictionary<string, object>()
                        {
                            { "@ssn", ssn }
                        },
                        message: out string message
                    );

                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];
                Patient_db instance = new ( 
                    row["ssn"].ToString(),
                    new Name_db(row["f_name"].ToString(), row["l_name"].ToString()),
                    row["phone_no"].ToString(),
                    row["address"].ToString(),
                    row["sex"].ToString(),
                    new Date_db(row["dob_year"].ToString(), row["dob_month"].ToString(), row["dob_day"].ToString()),
                    MakeEMR(row["emr_id"].ToString(), ssn, context)
                );

                statusResponse = new StatusResponse("Patient Retrieved successfully!");
                return instance;
            }
            catch (Exception ex)
            {
                statusResponse = new StatusResponse(ex);
                return null;
            }


        }
        
        public static EMR_db MakeEMR(string emr_id, int ssn, DbContext context)
        {
            EMR_db emr = new(ssn, emr_id);

            // here, create and add any existing symptom objects to emr.Record (that belong to emr_id)
            DataTable table = context.ExecuteDataQueryCommand
                (
                commandText: "select d.d_ssn, d.s_description, s.body_area, d.illness, d.diag_day, d.diag_month, d.diag_year, p.m_name, p.dosage\n" + 
                             "from emr as e\n" +
                             "join diagnosis as d on d.emr_id = e.emr_id\n"+
                             "join symptom as s on s.s_description = d.s_description\n" + 
                             "join prescription as p on d.diagnosis_no = p.diagnosis_no\n" + 
                             "where e.emr_id = @emr_id",

                parameters: new Dictionary<string, object>()
                {
                    {"@emr_id", emr_id }
                },
                message: out string message
                );


            if (table != null)
            {
                var record = new List<(Symptom_db symptom, Diagnosis_db diagnosis, Prescription_db prescription)>();

                foreach (DataRow row in table.Rows)
                {
                    (Symptom_db symptom, Diagnosis_db diagnosis, Prescription_db prescription) tuple = (
                         new Symptom_db(row["s_description"].ToString(), row["body_area"].ToString(), row["d_ssn"].ToString()),
                         new Diagnosis_db(row["illness"].ToString(), new Date_db(row["diag_year"].ToString(), row["diag_month"].ToString(), row["diag_day"].ToString())),
                         new Prescription_db(row["m_name"].ToString(), row["dosage"].ToString())
                         );

                    record.Add(tuple);
                }
                emr.Record = record;
            }
            return emr;

            
        }

        public static int Add(JsonElement data, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "BEGIN;\n" +
                                    "INSERT INTO person (SSN, phone_no, address, sex) VALUES (@ssn, @phone_no, @address, @sex);\n" +
                                    "INSERT INTO p_name (SSN, f_name, l_name) VALUES (@ssn, @f_name, @l_name);\n" +
                                    "INSERT INTO dob (SSN, dob_year, dob_month, dob_day) VALUES (@ssn, @dob_year, @dob_month, @dob_day);\n" +
                                    "INSERT INTO patient (SSN) VALUES (@ssn);\n" +
                                    "INSERT INTO emr (p_ssn, emr_id) VALUES (@ssn, @emr_id);\n" +
                                    "COMMIT;",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@ssn", data.GetProperty("ssn").GetString() },
                            { "@phone_no", data.GetProperty("phone_no").GetString() },
                            { "@address", data.GetProperty("address").GetString() },
                            { "@sex", data.GetProperty("sex").GetString() },
                            { "@f_name", data.GetProperty("name").GetProperty("f_name").GetString() },
                            { "@l_name", data.GetProperty("name").GetProperty("l_name").GetString() },
                            { "@dob_year", data.GetProperty("dob").GetProperty("year") },
                            { "@dob_month", data.GetProperty("dob").GetProperty("month") },
                            { "@dob_day", data.GetProperty("dob").GetProperty("day") },
                            { "@emr_id", Guid.NewGuid().ToString() }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                statusResponse = new StatusResponse("Patient added successfully");
                return rowsAffected;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return 0;
            }
        }

        public static int AddDiagnosis(int ssn, JsonElement data, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "BEGIN;\n" +
                                    "INSERT INTO diagnosis (d_ssn, s_description, illness, p_ssn, emr_id, diag_day, diag_month, diag_year) " +
                                    "VALUES (@d_ssn, @s_description, @illness, @p_ssn, " +
                                    "(SELECT e.emr_id FROM emr e WHERE e.p_ssn = @p_ssn), " +
                                    "@diag_day, @diag_month, @diag_year);\n" +
                                    "INSERT INTO prescription(d_ssn, m_name, dosage, p_ssn, emr_id, diagnosis_no) " +
                                    "VALUES (@d_ssn, @m_name, @dosage, @p_ssn, " +
                                    "(SELECT e.emr_id FROM emr e WHERE e.p_ssn = @p_ssn), " +
                                    "(SELECT LAST_INSERT_ID()));\n" +
                                    "COMMIT;",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@s_description", data.GetProperty("symptom").GetProperty("description").GetString() },
                            { "@body_area", data.GetProperty("symptom").GetProperty("body_area").GetString() },
                            { "@d_ssn", data.GetProperty("symptom").GetProperty("observer_SSN").GetString() },
                            { "@m_name", data.GetProperty("prescription").GetProperty("mname").GetString() },
                            { "@dosage", data.GetProperty("prescription").GetProperty("dosage").GetString() },
                            { "p_ssn", ssn },
                            { "@illness", data.GetProperty("diagnosis").GetProperty("condition").GetString() },
                            { "@diag_day", data.GetProperty("diagnosis").GetProperty("date").GetProperty("day").GetInt32() },
                            { "@diag_month", data.GetProperty("diagnosis").GetProperty("date").GetProperty("month").GetInt32() },
                            { "@diag_year", data.GetProperty("diagnosis").GetProperty("date").GetProperty("year").GetInt32() }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                statusResponse = new StatusResponse("Diagnosis added successfully");
                return rowsAffected;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return 0;
            }
        }

        // returns list of all patient SSNs
        public static List<string> GetCollection(DbContext context, out StatusResponse statusResponse)
        {

            try
            {
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "select p.ssn\n" +
                                     "from patient as p",
                        parameters: new Dictionary<string, object>() { },
                        message: out string message
                    );

                List<string> ssnList = new();

                foreach(DataRow row in table.Rows)
                {
                    ssnList.Add(row["ssn"].ToString());
                }

                statusResponse = new StatusResponse("Patients retrieved successfully");
                return ssnList;
            }
            catch (Exception ex)
            {
                statusResponse = new StatusResponse(ex);
                return null;
            }

        }
    }
}
