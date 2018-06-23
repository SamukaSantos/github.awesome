
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.View.Helpers;
using GitHub.Awesome.View.Interfaces;
using GitHub.Awesome.ViewModel.Base;
using GitHub.Awesome.ViewModel.Input;
using GitHub.Awesome.ViewModel.Interfaces;
using GitHub.Awesome.ViewTemplate.Styles;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GitHub.Awesome.ViewModel
{
	/// <summary>
	/// Class that represent Pull Request feature.
	/// </summary>
	public class PullRequestListViewModel : BaseViewModel<PullRequestItemViewModel>, IPullRequestListViewModel
	{
        #region Fields

        private IPullRequestApiService _pullRequestApiService;
		private FormattedString _formattedValues;
		      
        #endregion

        #region Properties

        /// <summary>
        /// PullRequestApiService Instance.
        /// </summary>
        public IPullRequestApiService PullRequestApiService
        {
            get
            {
                return _pullRequestApiService ?? (_pullRequestApiService = AppIoC.Container.Resolve<IPullRequestApiService>());
            }
        }
		/// <summary>
        /// PullRequestItemViewModel instance.
        /// </summary>
        public override PullRequestItemViewModel CurrentItem
        {
            get { return base.CurrentItem; }
            set
            {
                base.CurrentItem = value;
				RedirectTo(base.CurrentItem);
            }
        }

        public RepositoryItemViewModel RepositoryItem { get; private set; }

		public int Opened { get; private set; }

        public int Closed { get; private set; }

		public FormattedString FormattedValues
        {
			get{ return _formattedValues; }
			set{ SetField(ref _formattedValues, value); }
        }

        #endregion

        #region Constructor

        public PullRequestListViewModel(){ }

        public PullRequestListViewModel(RepositoryItemViewModel repositoryItem)
	    {
            RepositoryItem = repositoryItem;
        }

		public PullRequestListViewModel(IPageProxy pageProxy, RepositoryItemViewModel repositoryItem)
            : base(pageProxy)
        {
            RepositoryItem = repositoryItem;
        }
        
        #endregion

        #region Methods

		public override void OnAppearing()
        {
            FetchPullRequests();
        }
        
		private void FetchPullRequests()
        {
            Task.Run(async () =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;

				await FetchPullRequestsFromService();
                
                IsBusy = false;

            });
        }

		/// <summary>
        /// Fetch Pull Requests.
        /// </summary>
		public async Task FetchPullRequestsFromService()
        {  
			var response = await PullRequestApiService.GetPullRequests(RepositoryItem.PullsUrl,
                                                                       string.Empty,
			                                                           string.Empty);
            if (response.IsValid)
            {
				var items = response.ResultSet;

				Items = items;

				Opened = items.Count(it => it.State.ToLower().Equals(Constants.PullRequestState.Open));
				Closed = items.Count(it => !it.State.ToLower().Equals(Constants.PullRequestState.Open));

				FormattedValues =  new FormattedStringBuilder()
                                    .Span(new Span()
                                    {
                                        Text = $"{Opened} opened ",
                                        ForegroundColor = Color.FromHex("#de9307")
                                    })
                                    .Span("/")
                                    .Span(new Span()
                                    {
                                        Text = $" {Closed} closed",
                                        ForegroundColor = Color.Black
                                    }).Build();
            }
            else
            {
                ErrorPageHelper.New(PageProxy.Navigation, response.Notification.Error);
            }
        }

        private void RedirectTo(PullRequestItemViewModel currentItem)
        {
			if (currentItem == null) return;

			if (currentItem.User == null) return;

			if (string.IsNullOrWhiteSpace(currentItem.User.HtmlUrl)) return;

			Device.OpenUri(new System.Uri(currentItem.User.HtmlUrl));
        }

        #endregion
    }
}
