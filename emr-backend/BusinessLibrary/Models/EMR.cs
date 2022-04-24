using System;
using System.Collections.Generic;

namespace BusinessLibrary.Models
{
    public class EMR
    {
        #region Constructors
        // Default Constructor
        public EMR()
        { }

        // Constructor with Fields
        public EMR(int p_ssn, Guid emr_id, List<object> record)
        {
            P_ssn = p_ssn;
            Emr_id = emr_id;
            Record = record;
        }
        #endregion

        #region Properties
        public int P_ssn { get; set; }

        public Guid Emr_id { get; set; }

        // this is the record array
        // the intention is for it to have elements that are anonymous objects
        // each anonymous object is {Symptom, Diagnosis, Prescription}
        public List<object> Record{ get; set; }

        #endregion

    }
}
