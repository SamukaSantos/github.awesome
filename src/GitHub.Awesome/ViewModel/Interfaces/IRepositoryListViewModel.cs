
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.ViewModel.Base;
using GitHub.Awesome.ViewModel.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GitHub.Awesome.ViewModel.Interfaces
{
    /// <summary>
    /// Interface that represent Repository feature.
    /// </summary>
	public interface IRepositoryListViewModel : IViewModel
    {
        /// <summary>
        /// RepositoryApiService Instance.
        /// </summary>
        IRepositoryApiService RepositoryApiService { get; }
        /// <summary>
        /// Criteria of filtering.
        /// </summary>
        string SearchText { get; set; }
        /// <summary>
        /// Fetch repositories.
        /// </summary>
        Task FetchRepositoriesFromService();
		/// <summary>
        /// Fetch repositories with pagination behavior.
        /// </summary>
		void FetchRepositoriesFromServiceWithPagination();
        /// <summary>
        /// Repository intance.
        /// </summary>
        RepositoryItemViewModel CurrentItem { get; set; }

        /// <summary>
        /// Collection of Repositories
        /// </summary>
        /// <value>The items.</value>
		ObservableCollection<RepositoryItemViewModel> Items { get; set; }
    }
}
