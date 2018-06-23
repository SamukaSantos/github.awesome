
using GitHub.Awesome.Infra.Common.Notification;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Adapter of BackendResponseResult<TViewModel> from Backend to Client,
    /// using the same structure. This class result a collection of objects.
    /// </summary>
    public class ResponseResultCollection<TViewModel>
        where TViewModel : class, new()
    {
        #region Fields

        private BackendResponseResult _response;

        #endregion

        #region Properties

        /// <summary>
        /// Result set.
        /// </summary>
        public ObservableCollection<TViewModel> ResultSet { get; private set; }
        /// <summary>
        /// Notication object.
        /// </summary>
        public Notifiable Notification { get; private set; }
        /// <summary>
        /// Check for errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (_response.Errors == null)
                    return true;

                return _response.Errors.Count == 0;
            }
        }

        #endregion

        #region Constructor

        private ResponseResultCollection(BackendResponseResult response)
        {
            _response = response;

            Notification = new Notifiable();

            Mount(response);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates ResponseResultCollection<TViewModel> instance.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="response">Wrapped ResponseResultCollection<TViewModel> object.</param>
        /// <returns></returns>
        public static ResponseResultCollection<TViewModel> Create(BackendResponseResult response)
        {
            return new ResponseResultCollection<TViewModel>(response);
        }

        private void Mount(BackendResponseResult response)
        {
            if (IsValid)
            {
                var collection = JsonConvert.DeserializeObject<List<TViewModel>>(response.Content);

                ResultSet = new ObservableCollection<TViewModel>(collection);
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    Notification.AddNotification(new NotifiableItem
                    {
                        Key = error.Key,
                        Message = error.Message
                    });
                }
            }
        }

        #endregion
    }
}
