using System;
using System.Net;

namespace DatabaseLibrary.Core
{
    /// <summary>
    /// A customized error exception that includes the status code and the message.
    /// </summary>
    public class StatusException : Exception
    {

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public StatusException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The status code that represents the error.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Custom ToString to help with debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}", StatusCode.ToString(), Message);
        }

        #endregion

    }
}
