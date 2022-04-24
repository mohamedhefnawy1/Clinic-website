namespace BusinessLibrary.Models
{
    /// <summary>
    /// Response message used to reply back to our web API requests.
    /// </summary>
    public class ResponseMessage
    {

        #region Constructors

        /// <summary>
        /// Default constructor. 
        /// Need for serialization purposes.
        /// </summary>
        public ResponseMessage()
        {
        }

        /// <summary>
        /// Fields constructor.
        /// </summary>
        public ResponseMessage(bool state, string message, object data = null)
        {
            State = state;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Clone/Copy constructor.
        /// </summary>
        /// <param name="instance">The object to clone from.</param>
        public ResponseMessage(ResponseMessage instance)
            : this(instance.State, instance.Message, instance.Data)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// States whether the response contains data or not.
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Server message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data requested by the client.
        /// </summary>
        public object Data { get; set; }

        #endregion

    }
}
