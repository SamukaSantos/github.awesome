
using GitHub.Awesome.Infra.Backend;
using GitHub.Awesome.Infra.Services.Base;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.ViewModel.Input;
using System.Threading.Tasks;

namespace GitHub.Awesome.Infra.Services
{
    public class RepositoryApiService : BaseService, IRepositoryApiService
    {
        /// <summary>
        /// Fetch Repository items.
        /// </summary>
        /// <param name="url">Resource.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered ResponseResult of RepositoryCollectionViewModel.</returns>
        public async Task<ResponseResult<RepositoryCollectionViewModel>> GetRepositories(string url, string query, string authorizationToken)
        {   
            var result = await GetBackendConnector().GetDataAsync(url, query, authorizationToken);

            var response = ResponseResult<RepositoryCollectionViewModel>.Create(result);

            return response;
        }
    }
}
