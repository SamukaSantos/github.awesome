
using System;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewModel.Navigation
{
    /// <summary>
    /// Helper class to create pages.
    /// </summary>
    public class NavigationPageHelper
    {
        /// <summary>
        /// Create a navigation page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <returns>Content Page.</returns>
        public static NavigationPage Create(Page page)
        {
            return new NavigationPage(page) { BarTextColor = Color.White };
        }

        /// <summary>
        /// Create a page.
        /// </summary>
        /// <param name="pageType">Page type.</param>
        /// <returns>Content Page.</returns>
        public static Page CreateContentPage(Type pageType)
        {
			var page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

    }
}
