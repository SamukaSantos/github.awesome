
using System;

namespace GitHub.Awesome.Infra.Common.Exceptions
{
    /// <summary>
    /// Represents the base class of exception.
    /// </summary>
	public abstract class BaseException : Exception
    {
        #region Constructor

        protected BaseException() :
            base()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Notification for Exception.
        /// </summary>
        public abstract string Notification { get; }

        #endregion
    }
}
