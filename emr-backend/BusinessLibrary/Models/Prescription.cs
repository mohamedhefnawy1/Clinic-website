namespace BusinessLibrary.Models
{
    public class Prescription
    {
        #region Constructors

        public Prescription()
        {

        }

        public Prescription(string mname, string dosage)
        {
            Dosage = dosage;
            Mname = mname;
        }

        #endregion

        #region Properties

        public string Mname { get; set; }

        public string Dosage { get; set; }

        #endregion
    }
}
