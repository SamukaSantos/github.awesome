
using GitHub.Awesome.Infra.Caching;
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.Infra.Common.Paging;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.View.Helpers;
using GitHub.Awesome.View.Interfaces;
using GitHub.Awesome.View.Pages;
using GitHub.Awesome.ViewModel.Base;
using GitHub.Awesome.ViewModel.Input;
using GitHub.Awesome.ViewModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewModel
{
    /// <summary>
    /// Class that represent Repository feature.
    /// </summary>
    public class RepositoryListViewModel : BaseViewModel<RepositoryItemViewModel>, IRepositoryListViewModel
	{
		#region Fields

		private IRepositoryApiService _repositoryApiService;
		private string _searchText;
		private PagingManager _pagingManager;


        #endregion

        #region Properties

        /// <summary>
        /// RepositoryApiService Instance.
        /// </summary>
        public IRepositoryApiService RepositoryApiService
		{
			get 
			{
				return _repositoryApiService ?? (_repositoryApiService = AppIoC.Container.Resolve<IRepositoryApiService>());
            }
		}

		/// <summary>
        /// Repository intance.
        /// </summary>
        public override RepositoryItemViewModel CurrentItem
        {
            get { return base.CurrentItem; }
            set
            {
                base.CurrentItem = value;
                RedirectTo(base.CurrentItem);
            }
        }

        /// <summary>
        /// Criteria of filtering.
        /// </summary>
		public string SearchText
		{
			get { return _searchText; }
			set
			{
				SetField(ref _searchText, value);
				FilterRepositories(_searchText);
			}
		}

		#endregion

		#region Constructor
        
		public RepositoryListViewModel()
            : base() { }

		public RepositoryListViewModel(IPageProxy pageProxy)
			: base(pageProxy){ }

		#endregion

		#region Methods

		public override void OnAppearing()
		{
			FetchRepositories();

			ConfigurePaging();
		}


        private void ConfigurePaging()
        {
            _pagingManager = new PagingManager();

			_pagingManager.Add(new PagingItem { Page = 1, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 2, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 3, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 4, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 5, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 6, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 7, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 8, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 9, TotalPerPage = 100 });
			_pagingManager.Add(new PagingItem { Page = 10, TotalPerPage = 99 });         
        }

		private void FilterRepositories(string searchText)
		{
			if (!string.IsNullOrWhiteSpace(searchText))
			{
				var originalSource = SystemCache
										.Current
										.Get<ObservableCollection<RepositoryItemViewModel>>(Constants.Caching.Repository);

				var filteredItems = originalSource.Where(criteria => criteria.Name.ToLower().Contains(searchText.ToLower()) ||
														 criteria.Owner.Login.ToLower().Contains(searchText.ToLower()));

				Items = new ObservableCollection<RepositoryItemViewModel>(filteredItems);
			}         
		}

		private void FetchRepositories()
		{
			Task.Run(async () =>
			{
				if (IsBusy) 
					return;

				IsBusy = true;

				await FetchRepositoriesFromService();

				IsBusy = false;

			});
		}

        /// <summary>
        /// Fetch repositories.
        /// </summary>
		public async Task FetchRepositoriesFromService()
		{
			var cachedItems = SystemCache
				                .Current
				                .Get<ObservableCollection<RepositoryItemViewModel>>(Constants.Caching.Repository);

			if (cachedItems == null)
			{
				var response = await RepositoryApiService
					            .GetRepositories(Constants.Backend.ServiceApi.Repository,
				                                 string.Format(Constants.Backend.ServiceApi.RepositoryQuery, 
					                                           _pagingManager.Current.Page, 
					                                           _pagingManager.Current.TotalPerPage),
                                                 string.Empty);
				if (response.IsValid)
				{
					Items = response.Result.Items;

					Device.BeginInvokeOnMainThread(() =>
					{
						var date = DateTime.Now.AddMinutes(10);
					    var offset = new DateTimeOffset(date, TimeZoneInfo.Local.GetUtcOffset(date));

						SystemCache.Current.Set(Constants.Caching.Paging, _pagingManager.Position, offset);

						SystemCache.Current.Set(Constants.Caching.Repository, response.Result.Items, offset);
					});               
				}
				else
				{
					ErrorPageHelper.New(PageProxy.Navigation, response.Notification.Error);
				}
			}
			else
			{
				Items = cachedItems;
			}
		}

		public void FetchRepositoriesFromServiceWithPagination()
		{
			Task.Run(async () =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;

				var currentPosition = SystemCache.Current.Get<int>(Constants.Caching.Paging);

                _pagingManager.SetCurrent(currentPosition + 1);

				if(_pagingManager.Position <= _pagingManager.TotalItems)
				    await InternalFetchRepositoriesFromServiceWithPagination();

                IsBusy = false;

            });         
		}

		private async Task InternalFetchRepositoriesFromServiceWithPagination()
		{         
			var date = DateTime.Now.AddMinutes(10);

            var offset = new DateTimeOffset(date, TimeZoneInfo.Local.GetUtcOffset(date));

			SystemCache.Current.Set(Constants.Caching.Paging, _pagingManager.Position, offset);

			var cachedItems = SystemCache
				                .Current
                                .Get<ObservableCollection<RepositoryItemViewModel>>(Constants.Caching.Repository);


			var response = await RepositoryApiService
                                .GetRepositories(Constants.Backend.ServiceApi.Repository,
                                                 string.Format(Constants.Backend.ServiceApi.RepositoryQuery,
                                                               _pagingManager.Current.Page,
                                                               _pagingManager.Current.TotalPerPage),
                                                 string.Empty);
            if (response.IsValid)
            {   
                var items = response.Result.Items;

                foreach (var item in items)
					cachedItems.Add(item);

				Items = cachedItems;

				Device.BeginInvokeOnMainThread(() =>
                {               
					SystemCache.Current.Set(Constants.Caching.Repository, cachedItems, offset);
                });
            }
            else
            {
                ErrorPageHelper.New(PageProxy.Navigation, response.Notification.Error);
            }         
		}

        /// <summary>
        /// Redirect to PullRequestListPage.
        /// </summary>
        /// <param name="currentItem"></param>
        public async void RedirectTo(RepositoryItemViewModel currentItem)
        {
			if (currentItem == null) return;

			if (string.IsNullOrWhiteSpace(currentItem.Name)) return;

			await PageProxy.Navigation.PushAsync(new PullRequestListPage(currentItem));
        }

        
		#endregion
	}
}
