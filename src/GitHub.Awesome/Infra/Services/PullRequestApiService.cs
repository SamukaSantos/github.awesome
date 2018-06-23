
using GitHub.Awesome.Infra.Backend;
using GitHub.Awesome.Infra.Resources;
using GitHub.Awesome.Infra.Services.Base;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.ViewModel.Input;
using System.Threading.Tasks;

namespace GitHub.Awesome.Infra.Services
{
    /// <summary>
    /// Represents Pull Request Api.
    /// </summary>
    public class PullRequestApiService : BaseService, IPullRequestApiService
    {
        /// <summary>
        /// Fetch Pull Request items.
        /// </summary>
        /// <param name="url">Resource.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered ResponseResultCollection of Pull Requests.</returns>
        /// <returns></returns>
        public async Task<ResponseResultCollection<PullRequestItemViewModel>> GetPullRequests(string url, string query, string authorizationToken)
        {
			var resource = url.Replace(AppResources.REST_API_CORE_BASE_ADDRESS,"")
				              .Replace("{/number}", "");

            var result = await GetBackendConnector().GetDataAsync(resource, string.Empty, authorizationToken);

            var response = ResponseResultCollection<PullRequestItemViewModel>.Create(result);

            return response;
        }
    }
}
