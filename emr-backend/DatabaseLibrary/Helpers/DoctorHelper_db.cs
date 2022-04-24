using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace DatabaseLibrary.Helpers
{
    public class DoctorHelper_db
    {

        /// <summary>
        /// Get a single doctor from the database.
        /// </summary>
        public static Doctor_db Get(int ssn, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT d.*, c.c_name, b.dob_day, b.dob_month, b.dob_year, e.school, e.years, n.f_name, n.l_name, p.phone_no, p.address, p.sex " +
                                    "FROM doctor d " +
                                    "JOIN clinic c ON c.c_location = d.c_location " +
                                    "JOIN dob b ON b.SSN = d.SSN " +
                                    "JOIN educational_bg e ON e.d_ssn = d.SSN " +
                                    "JOIN p_name n ON n.SSN = d.SSN " +
                                    "JOIN person p ON p.SSN = d.SSN " +
                                    "WHERE d.SSN = @ssn",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@ssn", ssn }
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];
                Doctor_db instance = new (
                    row["ssn"].ToString(),
                    new Name_db(row["f_name"].ToString(), row["l_name"].ToString()),
                    row["phone_no"].ToString(),
                    row["address"].ToString(),
                    row["sex"].ToString(),
                    new Date_db(row["dob_year"].ToString(), row["dob_month"].ToString(), row["dob_day"].ToString()),
                    row["field"].ToString(),
                    new EducationalBackground_db(row["school"].ToString(), row["years"].ToString()),
                    new Clinic_db(row["c_location"].ToString(), row["c_name"].ToString())
                );

                // Return value
                statusResponse = new StatusResponse("Doctor retrieved successfully");
                return instance;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

        public static List<string> GetCollection(DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                // Get from database
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "SELECT d.ssn " +
                                    "FROM doctor d ",
                        parameters: new Dictionary<string, object>()
                        {
                        },
                        message: out string message
                    );
                if (table == null)
                    throw new Exception(message);

                List<string> ssnList = new List<string>();
                foreach (DataRow row in table.Rows)
                {
                    ssnList.Add(row["ssn"].ToString());
                }

                statusResponse = new StatusResponse("Doctors retrieved successfully");
                return ssnList;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return null;
            }
        }

        public static int Add(JsonElement data, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText:"BEGIN;\n" +
                                    "INSERT INTO person (SSN, phone_no, address, sex) VALUES (@ssn, @phone_no, @address, @sex);\n" +
                                    "INSERT INTO p_name (SSN, f_name, l_name) VALUES (@ssn, @f_name, @l_name);\n" +
                                    "INSERT INTO dob (SSN, dob_year, dob_month, dob_day) VALUES (@ssn, @dob_year, @dob_month, @dob_day);\n" +
                                    "INSERT INTO doctor (SSN, field, c_location) VALUES (@ssn, @field, @c_location);\n" +
                                    "INSERT INTO educational_bg (d_ssn, school, years) VALUES (@ssn, @school, @years);" +
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
                            { "@field", data.GetProperty("field").GetString() },
                            { "@c_location", data.GetProperty("c_location").GetString() },
                            { "@school", data.GetProperty("edu_bg").GetProperty("school").GetString() },
                            { "@years", data.GetProperty("edu_bg").GetProperty("years") }
                        },
                        message: out string message
                    );
                if (rowsAffected == -1)
                    throw new Exception(message);

                statusResponse = new StatusResponse("Doctor added successfully");
                return rowsAffected;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return 0;
            }
        }
    }
}
