
using GitHub.Awesome.View.Interfaces;

namespace GitHub.Awesome.ViewModel.Base
{
    /// <summary>
    /// Interface that represents a ViewModel.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Method that works with Xamarin.Forms OnAppearing method inside the viewmodel context.
        /// </summary>
		void OnAppearing();
        /// <summary>
        /// Method that works with Xamarin.Forms OnDisappearing method inside the viewmodel context.
        /// </summary>
        void OnDisappearing();
        /// <summary>
        /// Method that works with Xamarin.Forms OnPageChanged method inside the viewmodel context.
        /// Generally used with Navigation in MasterDetailPage and TabbedPage .
        /// </summary>
        void OnPageChanged();
        /// <summary>
        /// Method that works with Xamarin.Forms OnBindingContextChanged to initilize the bindable objects.
        /// </summary>
        void OnBindingContextChanged();
        /// <summary>
        /// Permits inject a new proxy implementation. This proxy is responsible for messaging and navigation.
        /// </summary>
        /// <param name="proxy"></param>
		void UpdateNavigationProxy(IPageProxy proxy);
    }
}
