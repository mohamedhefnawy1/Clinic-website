using System.Collections.Generic;

namespace DatabaseLibrary.Models
{
    public class Medication_db
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Medication_db()
        {

        }

        public Medication_db(string name, string side_effects, List<object> treats)
        {
            Name = name;
            SideEffects = side_effects;
            Treats = treats;
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string SideEffects { get; set; }

        public List<object> Treats { get; set; }

        #endregion

    }
}