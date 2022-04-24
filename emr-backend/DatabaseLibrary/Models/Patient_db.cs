namespace DatabaseLibrary.Models
{
    public class Patient_db : Person_db
    {
        #region Constructors

        // defualt constructor
        public Patient_db() : base()
        {

        }

        public Patient_db(string ssn, Name_db name, string phoneNumber, string address, string sex,
            Date_db dateOfBirth, EMR_db emr) :
            base(ssn, name, phoneNumber, address, sex, dateOfBirth)
        {
            Emr = emr;
        }
        #endregion

        #region Properties
        public EMR_db Emr { get; set; }
        #endregion

    }
}