using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class EMR_db
    {
        #region Constructors

        // default constructor
        public EMR_db()
        {
        }
        
        public EMR_db(int p_ssn, string emr_id)
        {
            P_ssn = p_ssn;
            Emr_id = emr_id;
        }
        #endregion

        #region Properties

        public int P_ssn { get; set; }
        public string Emr_id { get; set; }

        public List<(Symptom_db symptom, Diagnosis_db diagnosis, Prescription_db prescription)> Record { get; set; }

        #endregion


    }
}
