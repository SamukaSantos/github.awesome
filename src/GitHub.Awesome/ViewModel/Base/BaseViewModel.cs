
using GitHub.Awesome.Infra.Common.Notification;
using GitHub.Awesome.View.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GitHub.Awesome.ViewModel.Base
{
    /// <summary>
    /// Generic base viewmodel.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class BaseViewModel<TViewModel> : BaseViewModel, IViewModel
       where TViewModel : class, new()
    {

        #region Fields

        private bool _isBusy;
        private bool _isPullToRefreshBusy;
        private TViewModel _currentItem;
        private ObservableCollection<TViewModel> _items;

        #endregion

        #region Properties

        /// <summary>
        /// Checks if any operations is in execution.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetField(ref _isBusy, value); }
        }

        /// <summary>
        /// Responsible for controlling the Pull-To-Refresh behavior.
        /// </summary>
        public bool IsPullToRefreshBusy
        {
            get { return _isPullToRefreshBusy; }
            set { SetField(ref _isPullToRefreshBusy, value); }
        }

        /// <summary>
        /// IPageProxy instance.
        /// </summary>
        public IPageProxy PageProxy { get; private set; }

        /// <summary>
        /// Instance of Input viewmodel.
        /// </summary>
        public virtual TViewModel CurrentItem
        {
            get { return _currentItem; }
            set { SetField(ref _currentItem, value); }
        }

        /// <summary>
        /// Collection of TViewModel items. Commonly used with Xamarin.Forms.ListView widget.
        /// </summary>
        public virtual ObservableCollection<TViewModel> Items
        {
            get { return _items; }
            set { SetField(ref _items, value); }
        }

        #endregion

        #region Constructor

		public BaseViewModel(){ }

        public BaseViewModel(IPageProxy proxy)
        {
            PageProxy = proxy;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that works with Xamarin.Forms OnAppearing method inside the viewmodel context.
        /// </summary>
        public virtual void OnAppearing() { }

        /// <summary>
        /// Method that works with Xamarin.Forms OnDisappearing method inside the viewmodel context.
        /// </summary>
        public virtual void OnDisappearing() { }

        /// <summary>
        /// Method that works with Xamarin.Forms OnPageChanged method inside the viewmodel context.
        /// Generally used with Navigation in MasterDetailPage and TabbedPage .
        /// </summary>
        public virtual void OnPageChanged() { }

        /// <summary>
        /// Permits inject a new proxy implementation. This proxy is responsible for messaging and navigation.
        /// </summary>
        /// <param name="proxy"></param>
		public void UpdateNavigationProxy(IPageProxy proxy)
		{
			PageProxy = proxy;
		}

        /// <summary>
        /// Method that works with Xamarin.Forms OnBindingContextChanged to initilize the bindable objects.
        /// </summary>
        public virtual void OnBindingContextChanged()
        {
            CurrentItem = new TViewModel();

            Items = new ObservableCollection<TViewModel>();
        }
        

        #endregion
    }

    /// <summary>
    /// Base ViewModel.
    /// </summary>
    public abstract class BaseViewModel : Notifiable, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisedPropertyChanged<T>(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set a new value to the property.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="field">Current field reference.</param>
        /// <param name="value">New value.</param>
        /// <param name="propertyName">Property name</param>
        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                RaisedPropertyChanged<T>(propertyName);
            }
        }

        /// <summary>
        /// Helps to validate the current input viewmodel.
        /// </summary>
        /// <returns></returns>
        public override bool IsValid()
        {
            return base.IsValid();
        }

        #endregion
    }
}

