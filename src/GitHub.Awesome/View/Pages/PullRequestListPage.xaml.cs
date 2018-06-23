
using GitHub.Awesome.View.Base;
using GitHub.Awesome.ViewModel;
using GitHub.Awesome.ViewModel.Input;
using GitHub.Awesome.ViewModel.Interfaces;

namespace GitHub.Awesome.View.Pages
{
	public partial class PullRequestListPage : BaseContentPage
	{
		#region Constructor

		public PullRequestListPage(RepositoryItemViewModel repository)
		{
			InitializeComponent();

			SetContext<PullRequestListViewModel>(new object[] { this, repository });
		}

		#endregion

		#region Methods

		protected override void OnAppearing()
        {
            base.OnAppearing();

			lstPullRequests.ItemTapped += HandleItemTapped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

			lstPullRequests.ItemTapped -= HandleItemTapped;
        }

        void HandleItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
			if (BindingContext is IPullRequestListViewModel binding)
            {
				binding.CurrentItem = (PullRequestItemViewModel)e.Item;
            }
        }

		#endregion
	}
}