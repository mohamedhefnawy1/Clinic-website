using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Symptom_db
    {
        public Symptom_db()
        { }

        public Symptom_db(string s_description, string body_area, string observer_ssn)
        {
            S_description = s_description;
            Body_area = body_area;
            Observer_ssn = observer_ssn;
        }

        public string S_description { get; set; }

        public string Body_area { get; set; }

        public string Observer_ssn { get; set; }

    }
}
