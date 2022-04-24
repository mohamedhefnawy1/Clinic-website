namespace DatabaseLibrary.Models
{
    public class Person_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person_db()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Person_db(string ssn, Name_db name, string phoneNumber, string address, string sex, Date_db dateOfBirth)
        {
            SSN = ssn;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Sex = sex;
            DateOfBirth = dateOfBirth;
        }

        #endregion

        #region Properties

        public string SSN { get; set; }

        public Name_db Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Sex { get; set; }

        public Date_db DateOfBirth { get; set; }

        #endregion

    }
}
