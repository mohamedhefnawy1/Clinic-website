namespace BusinessLibrary.Models
{
    public class Diagnosis
    {
        #region Constructors
        public Diagnosis()
        {
        }

        // Constructor with fields.
        // I chose to use the DateOfBirth class to represent the date
        // I think in terms of naming it doesn't quite make sense, but the functionality is perfect
        public Diagnosis(string condition, Date date)
        {
            Condition = condition;
            Date = date;
        }
        #endregion

        #region Properties
        public string Condition { get; set; }

        public Date Date { get; set; }

        #endregion
 
    }
}
