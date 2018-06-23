
using System.Threading.Tasks;
using GitHub.Awesome.Infra.Backend;
using GitHub.Awesome.ViewModel.Input;

namespace GitHub.Awesome.Infra.Services.Interfaces
{
    /// <summary>
    /// Represents Repository Api.
    /// </summary>
    public interface IRepositoryApiService
    {
        /// <summary>
        /// Fetch Repository items.
        /// </summary>
        /// <param name="url">Resource.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered ResponseResult of RepositoryCollectionViewModel.</returns>
        Task<ResponseResult<RepositoryCollectionViewModel>> GetRepositories(string url, string query, string authorizationToken);
    }
}
