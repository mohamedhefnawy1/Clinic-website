using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Diagnosis_db
    {
        public Diagnosis_db()
        {
        }
        public Diagnosis_db(string illness, Date_db diagnosisDate)
        {
            Illness = illness;
            DiagnosisDate = diagnosisDate;
        }

        public string Illness { get; set; }

        public Date_db DiagnosisDate { get; set; }

    }
}
