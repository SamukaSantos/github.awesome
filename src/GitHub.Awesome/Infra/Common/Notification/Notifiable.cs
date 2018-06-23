
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GitHub.Awesome.Infra.Common.Exceptions;

namespace GitHub.Awesome.Infra.Common.Notification
{
    /// <summary>
    /// Class that represents the concept of domain validation.
    /// </summary>
    public class Notifiable
    {
        #region Fields

        private IList<NotifiableItem> _errors;

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Errors.
        /// </summary>
        public ReadOnlyCollection<NotifiableItem> Errors
        {
            get { return new ReadOnlyCollection<NotifiableItem>(_errors); }
        }

        /// <summary>
        /// First apparent error.
        /// </summary>
        public string Error
        {
            get
            {
                
                var error = _errors.FirstOrDefault();

                return error?.Message;
            }
        }

        #endregion

        #region Constructor

        public Notifiable()
        {
            _errors = new List<NotifiableItem>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Add notification.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="message">Message.</param>
        public void AddNotification(string key, string message)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(message))
            {
                var notifiableItem = new NotifiableItem { Key = key, Message = message };

                AddNotification(notifiableItem);
            }
        }

        /// <summary>
        /// Add notification.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="exception">Exception instance.</param>
        public void AddNotification(string key, BaseException exception)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(exception.Notification))
            {
                var notifiableItem = new NotifiableItem { Key = key, Message = exception.Notification };

                AddNotification(notifiableItem);
            }
        }

        /// <summary>
        /// Add notification.
        /// </summary>
        /// <param name="items">Collection of NotifiableItem.</param>
        public void AddNotifications(IEnumerable<NotifiableItem> items)
        {
            foreach (var item in items)
                AddNotification(item);
        }

        /// <summary>
        /// Add notification.
        /// </summary>
        /// <param name="item">NotifiableItem instance.</param>
        public void AddNotification(NotifiableItem item)
        {
            if (item != null)
                _errors.Add(item);
        }

        /// <summary>
        /// Checks for any errors.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            _errors.Clear();

            return true;
        }

        #endregion
  
    }
}
