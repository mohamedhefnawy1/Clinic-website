namespace DatabaseLibrary.Models
{
    public class Date_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Date_db()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Date_db(string year, string month, string day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        #endregion

        #region Properties

        public string Year { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        #endregion

    }
}
