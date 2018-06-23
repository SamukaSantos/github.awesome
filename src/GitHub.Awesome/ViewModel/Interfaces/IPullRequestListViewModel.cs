
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.ViewModel.Base;
using GitHub.Awesome.ViewModel.Input;

namespace GitHub.Awesome.ViewModel.Interfaces
{
    /// <summary>
    /// Interface that represent Pull Request feature.
    /// </summary>
	public interface IPullRequestListViewModel : IViewModel
    {
        /// <summary>
        /// PullRequestApiService Instance.
        /// </summary>
        IPullRequestApiService PullRequestApiService { get; }
        /// <summary>
        /// Repository root instance.
        /// </summary>
        RepositoryItemViewModel RepositoryItem { get; }
        /// <summary>
        /// PullRequestItemViewModel instance.
        /// </summary>
        PullRequestItemViewModel CurrentItem { get; set; }
    }
}
