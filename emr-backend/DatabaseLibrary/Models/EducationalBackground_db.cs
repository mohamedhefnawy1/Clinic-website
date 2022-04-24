namespace DatabaseLibrary.Models
{
    public class EducationalBackground_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EducationalBackground_db()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public EducationalBackground_db(string school, string years)
        {
            School = school;
            Years = years;
        }

        #endregion

        #region Properties

        public string School { get; set; }

        public string Years { get; set; }

        #endregion

    }
}
