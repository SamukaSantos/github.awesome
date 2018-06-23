
using GitHub.Awesome.ViewModel.Navigation;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Pages.Navigation
{
	public partial class RootPage : MasterDetailPage
	{
		#region Constructor

		public RootPage()
		{
			InitializeComponent();

			InitializeMasterDetail();
		}

		#endregion

		#region Methods

		protected override void OnAppearing()
        {
            base.OnAppearing();

            Coordinator.Selected += CoordinatorSampleSelected;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Coordinator.Selected -= CoordinatorSampleSelected;
        }

        private void InitializeMasterDetail()
        {
            Master = new MainMenuPage(new NavigationService(Navigation, LaunchSampleInDetail));
            Detail = NavigationPageHelper.Create(new RepositoryListPage());
        }

        private void LaunchSampleInDetail(Page page, bool animated)
        {
            Detail = NavigationPageHelper.Create(page);
            IsPresented = false;
        }

        private void CoordinatorSampleSelected(object sender, NavigatorEventArgs e)
        {
            if (e.Object.PageType == typeof(RootPage))
            {
                IsPresented = true;
            }
        }

		#endregion
	}
}
