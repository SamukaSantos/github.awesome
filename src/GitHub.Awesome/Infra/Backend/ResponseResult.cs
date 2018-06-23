
using GitHub.Awesome.Infra.Common.Notification;
using Newtonsoft.Json;
using System;

namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Adapter of BackendResponseResult from Backend to Client,
    /// using the same structure. This class result a simple response.
    /// </summary>
    public class ResponseResult
    {
        #region Fields

        private BackendResponseResult _response;

        #endregion

        #region Properties

        /// <summary>
        /// Status of response.
        /// </summary>
        public string Status { get; private set; }
        //// <summary>
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

        private ResponseResult(BackendResponseResult response)
        {
            _response = response;

            Notification = new Notifiable();

            Mount(response);
        }


        #endregion

        #region Methods

        /// <summary>
        /// Creates ResponseResult instance.
        /// </summary>
        /// <param name="response">Wrapped BackendResponseResult object.</param>
        /// <returns>Returns new instance of ResponseResult.</returns>
        public static ResponseResult Create(BackendResponseResult response)
        {
            return new ResponseResult(response);
        }

       private void Mount(BackendResponseResult response)
        {
            if (IsValid)
            {
                Status = response.Content.Equals("1") || response.Content.Equals("True") ? "Success" : "Error";
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

    /// <summary>
    /// Adapter of BackendResponseResult<TViewModel> from Backend to Client,
    /// using the same structure. This class validate a single object.
    /// </summary>
    public class ResponseResult<TViewModel>
        where TViewModel : class, new()
    {
        #region Fields

        private BackendResponseResult _response;

        #endregion

        #region Properties

        /// <summary>
        /// TViewModel result.
        /// </summary>
        public TViewModel Result { get; private set; }
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

        private ResponseResult(BackendResponseResult response)
        {
            _response = response;

            Notification = new Notifiable();

            Mount(response);
        }

        private ResponseResult(TViewModel viewmodel)
        {
            Result = viewmodel;

            Notification = new Notifiable();

            Mount(viewmodel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates ResponseResult<TViewModel> instance.
        /// </summary>
        /// <param name="response">>Wrapped BackendResponseResult object.</param>
        /// <returns>Returns new instance of ResponseResult<TViewModel>.</returns>
        public static ResponseResult<TViewModel> Create(BackendResponseResult response)
        {
            return new ResponseResult<TViewModel>(response);
        }

        /// <summary>
        /// Creates ResponseResult<TViewModel> instance.
        /// </summary>
        /// <param name="response">TViewModel instance</param>
        /// <returns>Returns new instance of ResponseResult<TViewModel>.</returns>
        public static ResponseResult<TViewModel> Create(TViewModel response)
        {
            return new ResponseResult<TViewModel>(response);
        }

        private void Mount(BackendResponseResult response)
        {
            if (IsValid)
            {
                Result = JsonConvert.DeserializeObject<TViewModel>(response.Content.ToString());
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

        private void Mount(TViewModel response)
        {
            if (response == null)
            {
                Notification.AddNotification(new NotifiableItem
                {
                    Key = typeof(TViewModel).FullName,
                    Message = new NullReferenceException().Message
                });
            }
        }


        #endregion
    }
}
