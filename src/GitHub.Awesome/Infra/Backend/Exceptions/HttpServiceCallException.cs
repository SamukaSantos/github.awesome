

using GitHub.Awesome.Infra.Common.Exceptions;

namespace GitHub.Awesome.Infra.Backend.Exceptions
{
    /// <summary>
    /// Exception of generic service call.
    /// </summary>
    public class HttpServiceCallException : BaseException
    {
        #region Properties

        /// <summary>
        /// Status code.
        /// </summary>
        public string HttpStatusCode { get; private set; }
        /// <summary>
        /// Notication message.
        /// </summary>
        public override string Notification
        {
            get { return HttpStatusCode; }
        }

        #endregion

        #region Constructor

        public HttpServiceCallException(string httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        #endregion

    }
}
