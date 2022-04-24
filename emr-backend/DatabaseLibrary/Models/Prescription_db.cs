using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Prescription_db
    {
        public Prescription_db() { }

        public Prescription_db(string m_name, string dosage) 
        {
            M_name = m_name;
            Dosage = dosage;
        }

        public string M_name { get; set; }

        public string Dosage { get; set; }

    }
}
