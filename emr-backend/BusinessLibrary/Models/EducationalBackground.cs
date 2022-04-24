namespace BusinessLibrary.Models
{
    public class EducationalBackground
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EducationalBackground()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public EducationalBackground(string school, string years)
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
