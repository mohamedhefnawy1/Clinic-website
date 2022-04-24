namespace BusinessLibrary.Models
{
    public class Doctor : Person
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Doctor() : base()
        {

        }

        public Doctor(int ssn, Name name, string phoneNumber, string address, string sex, Date dateOfBirth,
            string field, EducationalBackground educationalBackground, Clinic clinic) :
            base(ssn, name, phoneNumber, address, sex, dateOfBirth)
        {
            Field = field;
            EducationalBackground = educationalBackground;
            Clinic = clinic;
        }

        #endregion

        #region Properties

        public string Field { get; set; }

        public EducationalBackground EducationalBackground { get; set; }

        public Clinic Clinic { get; set; }

        #endregion

    }
}
