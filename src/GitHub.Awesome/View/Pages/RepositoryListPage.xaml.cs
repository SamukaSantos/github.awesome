
using GitHub.Awesome.View.Base;
using GitHub.Awesome.ViewModel.Input;
using GitHub.Awesome.ViewModel.Interfaces;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Pages
{
	public partial class RepositoryListPage : BaseContentPage
	{
		
		#region Constructor

		public RepositoryListPage()
		{
			InitializeComponent();

			SetContext<IRepositoryListViewModel>(this);
		}

		#endregion

		#region Methods

		protected override void OnAppearing()
		{
			base.OnAppearing();

			lstRepositories.ItemTapped += HandleItemTapped;
			lstRepositories.ItemAppearing += OnItemAppearing;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			lstRepositories.ItemTapped    -= HandleItemTapped;
			lstRepositories.ItemAppearing -= OnItemAppearing;
		}

		void HandleItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {         
			if (BindingContext is IRepositoryListViewModel binding)
            {
                binding.CurrentItem = (RepositoryItemViewModel)e.Item;
            }
		}
        
        void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

			if (BindingContext is IRepositoryListViewModel binding)
            {
				var currentItem = e.Item as RepositoryItemViewModel;

				var lastItem = binding.Items[binding.Items.Count - 1];

                if (currentItem == lastItem)
                {
					binding.FetchRepositoriesFromServiceWithPagination();
                }
            }
        }

		#endregion
	}
}