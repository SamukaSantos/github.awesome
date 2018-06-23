
using GitHub.Awesome.View.Pages.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewModel.Navigation
{
	public class NavigationObject : ObservableObject
    {
        #region Fields

        private readonly bool _modal;
        
        #endregion

        #region Properties

        /// <summary>
        /// Menu option name.
        /// </summary>
		public string Name { get; private set; }
        /// <summary>
        /// Menu option icon.
        /// </summary>
		public string Icon { get; private set; }
        /// <summary>
        /// Menu option background image.
        /// </summary>
		public string BackgroundImage { get; private set; }
        /// <summary>
        /// Menu option page type item.
        /// </summary>
		public Type PageType { get; private set; }
     
        #endregion

        #region Constructor

        
        public NavigationObject(string name,
                                Type pageType,
                                string backgroundImage,
                                string icon,
                                bool modal = false)
        {
			Name = name;
			PageType = pageType;
			BackgroundImage = backgroundImage;
			Icon = icon;
            _modal = modal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the structure of masterdetailpage navigation.
        /// </summary>
        /// <param name="navigation">INavigation instance.</param>
        public async Task NavigateTo(INavigation navigation)
        {
            Coordinator.RaiseSelected(this);
            
            int popCount            = 0;
            int firstPageToPopIndex = 0;

            if (PageType == typeof(RootPage))
            {
                popCount = navigation.NavigationStack.Count - 1;
            }
            else
            {
                for (int i = navigation.NavigationStack.Count - 1; i >= 0; i--)
                {
                    if (navigation.NavigationStack[i].GetType() == PageType)
                    {
                        firstPageToPopIndex = i + 1;
                        popCount = navigation.NavigationStack.Count - 1 - i;
                        break;
                    }
                }
            }

            if (popCount > 0)
            {
                for (int i = 1; i < popCount; i++)
                {
                    navigation.RemovePage(navigation.NavigationStack[firstPageToPopIndex]);
                }

                await navigation.PopAsync();

                return;
            }

            var page = NavigationPageHelper.CreateContentPage(PageType);

            if (_modal)
            {
                await navigation.PushModalAsync(NavigationPageHelper.Create(page));
            }
            else
            {
                await navigation.PushAsync(page);
            }
        }

        #endregion
    }
}
