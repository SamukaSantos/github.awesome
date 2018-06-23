
using GitHub.Awesome.Infra.Backend;
using GitHub.Awesome.ViewModel.Input;
using System.Threading.Tasks;

namespace GitHub.Awesome.Infra.Services.Interfaces
{
    /// <summary>
    /// Represents Pull Request Api.
    /// </summary>
    public interface IPullRequestApiService
    {
        /// <summary>
        /// Fetch Pull Request items.
        /// </summary>
        /// <param name="url">Resource.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered ResponseResultCollection of Pull Requests.</returns>
        Task<ResponseResultCollection<PullRequestItemViewModel>> GetPullRequests(string url, string query, string authorizationToken);
    }
}
