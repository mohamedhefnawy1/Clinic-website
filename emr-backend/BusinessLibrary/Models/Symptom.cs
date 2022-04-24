namespace BusinessLibrary.Models
{
    public class Symptom
    {
        #region Constructors

        public Symptom()
        {
        }

        public Symptom(string description, string body_area, int observer_ssn)
        {
            Description = description;
            Body_area = body_area;
            Observer_SSN = observer_ssn;
        }
        #endregion

        #region Properties

        public string Description { get; set; }

        public string Body_area { get; set; }

        public int Observer_SSN { get; set; }

        #endregion
    }
}
