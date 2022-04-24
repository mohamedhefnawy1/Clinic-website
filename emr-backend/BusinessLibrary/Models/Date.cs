using System.Text.Json.Serialization;

namespace BusinessLibrary.Models
{
    public class Date
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Date()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        #endregion

        #region Properties

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        #endregion

    }
}
