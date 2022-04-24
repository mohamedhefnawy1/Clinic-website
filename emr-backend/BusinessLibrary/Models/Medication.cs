using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessLibrary.Models
{
    public class Medication
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Medication()
        {

        }

        public Medication(string name, string side_effects, List<object> treats)
        {
            Name = name;
            SideEffects = side_effects;
            Treats = treats;
        }

        #endregion

        #region Properties

        [JsonPropertyName("m_name")]
        public string Name { get; set; }

        [JsonPropertyName("side_effects")]
        public string SideEffects { get; set; }

        [JsonPropertyName("treats")]
        public List<object> Treats { get; set; }

        #endregion

    }
}
