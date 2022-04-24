

namespace DatabaseLibrary.Models
{
    public class Doctor_db : Person_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Doctor_db() : base()
        {

        }

        public Doctor_db(string ssn, Name_db name, string phoneNumber, string address, string sex, Date_db dateOfBirth,
            string field, EducationalBackground_db educationalBackground, Clinic_db clinic) : 
            base(ssn, name, phoneNumber, address, sex, dateOfBirth)
        {
            Field = field;
            EducationalBackground = educationalBackground;
            Clinic = clinic;
        }

        #endregion

        #region Properties

        public string Field { get; set; }

        public EducationalBackground_db EducationalBackground { get; set; }

        public Clinic_db Clinic { get; set; }

        #endregion

    }
}
