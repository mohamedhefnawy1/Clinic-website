using DatabaseLibrary.Core;
using DatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace DatabaseLibrary.Helpers
{
    public class MedicationHelper_db
    {
        public static Medication_db Get(string name, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                DataTable table = context.ExecuteDataQueryCommand
                    (
                    commandText: "select  m.* \n " +
                                    "from medication as m \n " +
                                    "where m.m_name = @name",

                        parameters: new Dictionary<string, object>()
                        {
                            { "@name", name }
                        },
                        message: out string message
                    );

                if (table == null)
                    throw new Exception(message);

                DataRow row = table.Rows[0];
                Medication_db instance = new Medication_db(
                    row["m_name"].ToString(),
                    row["side_effects"].ToString(),
                    GetTreats(name, context)
                );

                // this is the return value
                statusResponse = new StatusResponse("Medication Retrieved successfully!");
                return instance;
            }
            catch (Exception ex)
            {
                statusResponse = new StatusResponse(ex);
                return null;
            }


        }
        public static List<object> GetTreats(string name, DbContext context)
        {
            DataTable table = context.ExecuteDataQueryCommand
                    (
                    commandText: "select  t.s_description \n " +
                                    "from treats as t \n " +
                                    "where t.m_name = @name",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@name", name }
                        },
                        message: out string message
                    );
            List<object> symptoms = new List<object>();

            foreach (DataRow row in table.Rows)
            {
                symptoms.Add(new { symptom = row["s_description"].ToString() });
            }
            return symptoms;
        }

        public static int AddSymptoms(JsonElement data, DbContext context)
        {
            int rowsAffected = 0;
            var treats = data.EnumerateArray();
            while(treats.MoveNext())
            {
                var symptom = treats.Current;
                rowsAffected += context.ExecuteNonQueryCommand
                   (
                       commandText: "BEGIN;\n" +
                                   "INSERT INTO symptom (s_description, body_area) VALUES (@s_description, @body_area);\n" +
                                   "COMMIT;",
                       parameters: new Dictionary<string, object>()
                       {
                            { "@s_description", symptom.GetProperty("symptom").GetString() },
                            { "@body_area", symptom.GetProperty("body_area").GetString() }
                       },
                       message: out string message
                   );
            }
            return rowsAffected;
        }

        public static int AddTreats(string m_name, JsonElement data, DbContext context)
        {
            int rowsAffected = 0;
            var treats = data.EnumerateArray();
            while (treats.MoveNext())
            {
                var symptom = treats.Current;
                rowsAffected += context.ExecuteNonQueryCommand
                   (
                       commandText: "BEGIN;\n" +
                                   "INSERT INTO treats (m_name, s_description) VALUES (@m_name, @s_description);\n" +
                                   "COMMIT;",
                       parameters: new Dictionary<string, object>()
                       {
                            { "@s_description", symptom.GetProperty("symptom").GetString() },
                            { "@m_name", m_name }
                       },
                       message: out string message
                   );
            }
            return rowsAffected;
        }

        public static int Add(JsonElement data, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                AddSymptoms(data.GetProperty("treats"), context);
                int rowsAffected = context.ExecuteNonQueryCommand
                    (
                        commandText: "BEGIN;\n" +
                                    "INSERT INTO medication (m_name, side_effects) VALUES (@m_name, @side_effects);\n" +
                                    "COMMIT;",
                        parameters: new Dictionary<string, object>()
                        {
                            { "@m_name", data.GetProperty("m_name").GetString() },
                            { "@side_effects", data.GetProperty("side_effects").GetString() }
                        },
                        message: out string message
                    );
                AddTreats(data.GetProperty("m_name").ToString(), data.GetProperty("treats"), context);
                if (rowsAffected == -1)
                    throw new Exception(message);

                statusResponse = new StatusResponse("Medication added successfully");
                return rowsAffected;
            }
            catch (Exception exception)
            {
                statusResponse = new StatusResponse(exception);
                return 0;
            }
        }
        public static List<string> GetCollection(DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "select m.m_name\nfrom medication as m",
                        parameters: new Dictionary<string, object>() { },
                        message: out string message
                    );
                List<string> medicationNameList = new();
                
                foreach(DataRow row in table.Rows)
                {
                    medicationNameList.Add(row["m_name"].ToString());
                }

                statusResponse = new StatusResponse("Medication Names Retrieved Successfully!");
                return medicationNameList;
            }
            catch (Exception ex)
            {
                statusResponse = new StatusResponse(ex);
                return null;

            }
        }

        public static List<string> GetWhatTreats(string symptom, DbContext context, out StatusResponse statusResponse)
        {
            try
            {
                DataTable table = context.ExecuteDataQueryCommand
                    (
                        commandText: "select t.m_name\nfrom treats as t\nwhere t.s_description = @symptom",
                        parameters: new Dictionary<string, object>()
                            {
                                {"@symptom", symptom }
                            },
                        message: out string message
                    );
                List<string> treatsList = new();

                foreach(DataRow row in table.Rows)
                {
                    treatsList.Add(row["m_name"].ToString());
                }
                statusResponse = new StatusResponse("List of what medication treats this symptom Retrieved Successfully!");
                return treatsList;


            }
            catch (Exception ex)
            {
                statusResponse = new StatusResponse(ex);
                return null;
            }
        }
    }
}
