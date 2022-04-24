namespace DatabaseLibrary.Models
{
    public class Name_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Name_db()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Name_db(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion

    }
}
