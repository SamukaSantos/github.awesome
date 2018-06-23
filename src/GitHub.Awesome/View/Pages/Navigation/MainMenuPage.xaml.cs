
using CotaFacil.Mobile.ViewModel.Navigatiion;
using GitHub.Awesome.View.Base;
using GitHub.Awesome.ViewModel.Navigation;
using GitHub.Awesome.ViewModel.Navigation.Interfaces;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Pages.Navigation
{
	public partial class MainMenuPage : BaseContentPage
	{
		#region Fields

		private readonly INavigation _navigation;

		#endregion

		#region Constructor

		public MainMenuPage(INavigation navigation)
		{
			InitializeComponent();

			_navigation = navigation;

			SetContext<IMainMenuViewModel>(this);
		}

		#endregion

		#region Methods

		public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = optionItems.SelectedItem as NavigationObject;

            if (item != null)
            {
                if (item.PageType != typeof(RootPage))
                {
                    await item.NavigateTo(_navigation);
                }

                optionItems.SelectedItem = null;
            }
        }

		#endregion
	}
}
