namespace BusinessLibrary.Models
{
    public class Patient : Person
    {
        #region Constructors
        public Patient() : base()
        {

        }

        public Patient(int ssn, Name name, string phoneNumber, string address, string sex, Date dateOfBirth, EMR? emr) :
        base(ssn, name, phoneNumber, address, sex, dateOfBirth)
        {
            Emr = emr;
        }
        #endregion
        #region Properties

        public EMR Emr { get; set; }

        #endregion
    }
}

