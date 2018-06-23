using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewModel.Navigation
{
    /// <summary>
    /// Custom implementation of Xamarin.Forms INavigation interface.
    /// </summary>
    public class NavigationService: INavigation
    {
        #region Fields

        private readonly INavigation _navigation;
        private readonly Action<Page, bool> _pushAsyncReplacement;

        #endregion

        #region Properties

        /// <summary>
        /// Current Navigation Stack.
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<Page> NavigationStack
        {
            get
            {
                return _navigation.NavigationStack;
            }
        }

        /// <summary>
        /// Current Modal Navigation Stack.
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<Page> ModalStack
        {
            get
            {
                return _navigation.ModalStack;
            }
        }

        #endregion

        #region Constructor

        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public NavigationService(INavigation navigation, Action<Page, bool> pushAsyncReplacement) : this(navigation)
        {
            _pushAsyncReplacement = pushAsyncReplacement;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Push the current page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <returns></returns>
        public Task PushAsync(Page page)
        {
            if (_pushAsyncReplacement != null)
            {
                return BeginInvokeOnMainThreadAsync(() => _pushAsyncReplacement(page, true));
            }

            return _navigation.PushAsync(page);
        }

        /// <summary>
        /// Push the current page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <param name="animated">Enable animation.</param>
        /// <returns></returns>
        public Task PushAsync(Page page, bool animated)
        {
            if (_pushAsyncReplacement != null)
            {
                return BeginInvokeOnMainThreadAsync(() => _pushAsyncReplacement(page, animated));
            }

            return _navigation.PushAsync(page, animated);
        }

        /// <summary>
        /// Pop the top page of the navigation stack.
        /// </summary>
        /// <returns></returns>
        public Task<Page> PopAsync()
        {
            if (_pushAsyncReplacement != null)
            {
                throw new NotImplementedException();
            }

            return _navigation.PopAsync();
        }

        /// <summary>
        /// Pop the top page of the navigation stack.
        /// </summary>
        /// <param name="animated">Enable animation.</param>
        /// <returns></returns>
        public Task<Page> PopAsync(bool animated)
        {
            if (_pushAsyncReplacement != null)
            {
                throw new NotImplementedException();
            }

            return _navigation.PopAsync(animated);
        }

        /// <summary>
        /// Pop to root page.
        /// </summary>
        /// <returns></returns>
        public Task PopToRootAsync()
        {
            return _navigation.PopToRootAsync();
        }

        /// <summary>
        /// Pop to root page.
        /// </summary>
        /// <param name="animated">Enable animation.</param>
        /// <returns></returns>
        public Task PopToRootAsync(bool animated)
        {
            return _navigation.PopToRootAsync(animated);
        }

        /// <summary>
        /// Push the current modal page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <returns></returns>
        public Task PushModalAsync(Page page)
        {
            return _navigation.PushModalAsync(page);
        }

        /// <summary>
        /// Push the current modal page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <param name="animated">Enable animation.</param>
        /// <returns></returns>
        public Task PushModalAsync(Page page, bool animated)
        {
            return _navigation.PushModalAsync(page, animated);
        }

        /// <summary>
        /// Pop the top page of the modal navigation stack.
        /// </summary>
        /// <returns></returns>
        public Task<Page> PopModalAsync()
        {
            return _navigation.PopModalAsync();
        }

        /// <summary>
        /// Pop the top page of the modal navigation stack.
        /// </summary>
        /// <param name="animated">Enable animation.</param>
        /// <returns></returns>
        public Task<Page> PopModalAsync(bool animated)
        {
            return _navigation.PopModalAsync(animated);
        }

        /// <summary>
        /// Remove page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        public void RemovePage(Page page)
        {
            _navigation.RemovePage(page);
        }

        /// <summary>
        /// Insert a new page before another one.
        /// </summary>
        /// <param name="page">New page.</param>
        /// <param name="before">Page reference.</param>
        public void InsertPageBefore(Page page, Page before)
        {
            _navigation.InsertPageBefore(page, before);
        }

        /// <summary>
        /// Method that execute a specific operation instead of the default navigation.
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns></returns>
        public static Task BeginInvokeOnMainThreadAsync(Action action)
        {
            TaskCompletionSource<object> completitionSource = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    completitionSource.SetResult(null);
                }
                catch (Exception ex)
                {
                    completitionSource.SetException(ex);
                }
            });
            return completitionSource.Task;
        }

        #endregion
    }
}
