using System.Text.Json.Serialization;

namespace BusinessLibrary.Models
{
    public class Clinic
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Clinic()
        {

        }

        /// <summary>
        /// Constructor with fields.
        /// </summary>
        public Clinic(string location, string name)
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
