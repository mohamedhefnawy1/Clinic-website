namespace DatabaseLibrary.Models
{
    public class Clinic_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Clinic_db()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Clinic_db(string location, string name)
        {
            Location = location;
            Name = name;
        }

        #endregion

        #region Properties

        public string Location { get; set; }
        
        public string Name { get; set; }

        #endregion
    }
}
