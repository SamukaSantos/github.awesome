


using GitHub.Awesome.Infra.Backend;

namespace GitHub.Awesome.Infra.Services.Base
{
    /// <summary>
    /// Base class for APIs.
    /// </summary>
    public class BaseService
    {
        #region Fields

        protected BackendConnector _connector;

        #endregion

        #region Methods

        /// <summary>
        /// Get instance of BackendConnector.
        /// </summary>
        /// <returns>BackendConnector instance.</returns>
        protected virtual BackendConnector GetBackendConnector()
        {
            if (_connector == null)
            {
                _connector = new BackendConnector();
            }

            return _connector;
        }

        /// <summary>
        /// Get instance of BackendConnector.
        /// </summary>
        /// <param name="baseAddress">Base address.</param>
        /// <returns>BackendConnector instance.</returns>
        protected virtual BackendConnector GetBackendConnector(string baseAddress)
        {
            if (_connector == null)
            {
                _connector = new BackendConnector(baseAddress);
            }

            return _connector;
        }

        #endregion
    }
}
