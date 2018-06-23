
using GitHub.Awesome.Infra.Common.Exceptions;

namespace GitHub.Awesome.Infra.Backend.Exceptions
{
    /// <summary>
    /// Exception thats represents offline state.
    /// </summary>
    public class OffLineException : BaseException
    {
        #region Properties

        /// <summary>
        /// Error message.
        /// </summary>
        public override string Message
        {
            get { return "No connection. Check your internet and try it again."; }         
        }
        /// <summary>
        /// Notication message.
        /// </summary>
        public override string Notification
        {
            get { return string.Empty; }
        }

        #endregion

        #region Constructor

        public OffLineException()
            : base() { }

        #endregion
    }
}
