using System.Text.Json.Serialization;

namespace BusinessLibrary.Models
{
    public class Person
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Person(int ssn, Name name, string phoneNumber, string address, string sex, Date dateOfBirth)
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

        public int SSN { get; set; }

        public Name Name { get; set; }

        [JsonPropertyName("phone_no")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Sex { get; set; }

        [JsonPropertyName("dob")]
        public Date DateOfBirth { get; set; }

        #endregion

    }
}
