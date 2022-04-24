using System.Text.Json.Serialization;

namespace BusinessLibrary.Models
{
    public class Name
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Name()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Properties

        [JsonPropertyName("f_name")]
        public string FirstName { get; set; }


        [JsonPropertyName("l_name")]
        public string LastName { get; set; }

        #endregion

    }
}
