
using GitHub.Awesome.Infra.Backend.Enum;
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.Infra.Common.Notification;
using GitHub.Awesome.Infra.Interfaces;
using GitHub.Awesome.Infra.Resources;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Class responsible for make remote service call based on HttpVerbs.
    /// </summary>
    public class BackendConnector : Notifiable
    {
        #region Fields

        private string _baseAddress;
		private static HttpClient _httpClient;
        private INetworkConnectivity _networkConnectivity;
        
        #endregion

        #region Properties

        /// <summary>
        /// Checks the connectivity.
        /// </summary>
        public INetworkConnectivity NetworkConnectivity
        {
            get { return _networkConnectivity ?? (_networkConnectivity = AppIoC.Container.Resolve<INetworkConnectivity>()); }
        }

        public string this[string key]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(key)) return string.Empty;

                var resourceManager = AppResources.ResourceManager;

                return resourceManager.GetString(key);
            }
        }

        #endregion

        #region Constructor

        public BackendConnector(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public BackendConnector()
        {
            _baseAddress = this["REST_API_CORE_BASE_ADDRESS"];
        }

        #endregion

        #region Methods

        private HttpClient GetClient(string authorizationToken = null)
        {
			if (_httpClient == null)
				_httpClient = new HttpClient();

			_httpClient.DefaultRequestHeaders.Clear();         

            if (!string.IsNullOrWhiteSpace(authorizationToken))
                _httpClient.DefaultRequestHeaders.Add(Constants.Backend.Methods.AUTHORIZATION_KEY, authorizationToken);

			_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", Constants.Backend.ServiceApi.UserAgent);

            return _httpClient;
        }

        private BackendResponseResult ParseToBackendResponse(string response, ERequestMethod method, ERequestResult result)
        {
            try
            {
                var backendResponseResult = new BackendResponseResult
                {
                    Method = method.ToString(),
                    Result = result.ToString(),
                    Errors = null,
                    Content = response,
                    StatusCode = HttpStatusCode.OK
                };

                return backendResponseResult;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Method responsible for fetch items, using HttpVerb GET.
        /// </summary>
        /// <param name="url">Base Url.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public async Task<BackendResponseResult> GetDataAsync(string url, string query = "", string authorizationToken = "")
        {
            //if (!NetworkConnectivity.CheckNetworkConnection())
			if(!CrossConnectivity.Current.IsConnected)
            {
                return ERequestMethod.GET.Offline();
            }
            else
            {
                var uri = new Uri($"{_baseAddress}{url}{query}");

                try
                {
                    var response = await GetClient(authorizationToken).GetAsync(uri);

                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return ParseToBackendResponse(content, ERequestMethod.GET, ERequestResult.Success) ??
                            ERequestMethod.GET.Failure(Constants.Backend.Methods.GET_DATA_ASYNC_KEY, HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        return ERequestMethod.GET.Failure(Constants.Backend.Methods.GET_DATA_ASYNC_KEY, response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    if(e != null)
                        if(e.Message.Equals("An error occurred while sending the request"))
                            return ERequestMethod.GET.Failure(HttpStatusCode.InternalServerError, e.Source, AppResources.STATUS_CODE_INTERNAL_SERVER_ERROR);

                    return ERequestMethod.GET.Failure(HttpStatusCode.InternalServerError, e.Source, e.Message);
                }
            }
        }
        
        /// <summary>
        /// Method responsible for fetch items, using HttpVerb GET.
        /// </summary>
        /// <param name="url">Base Url.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Raw string content.</returns>
        public async Task<string> GetStringJsonDataAsync(string url, string query = "", string authorizationToken = "")
        {
            //if (!NetworkConnectivity.CheckNetworkConnection())
			if (!CrossConnectivity.Current.IsConnected)
            {
                return "Offline";
            }
            else
            {
                var uri = new Uri($"{_baseAddress}{url}{query}");

                var response = await GetClient(authorizationToken).GetAsync(uri);

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return content;
                }
            }
            return null;
        }

        /// <summary>
        /// Method responsible for execute operations, using HttpVerbs POST or PUT.
        /// </summary>
        /// <typeparam name="T">Type of class.</typeparam>
        /// <param name="item">Instance of the class.</param>
        /// <param name="method">HttpVerb.</param>
        /// <param name="url">Base Url.</param>
        /// <param name="query">Parameters.</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token).</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public async Task<BackendResponseResult> SaveOrUpdateAsync<T>(T item, ERequestMethod method, string url, string queryString = "", string authorizationToken = "")
        {
            //if (!NetworkConnectivity.CheckNetworkConnection())
			if (!CrossConnectivity.Current.IsConnected)
            {
                return ERequestMethod.POST.Offline();
            }
            else
            {
                try
                {
                    var uri = new Uri(string.Concat(_baseAddress, url));

                    var json = JsonConvert.SerializeObject(item, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                  
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;

                    if (method == ERequestMethod.POST)
                    {
                        response = await GetClient(authorizationToken).PostAsync(uri, content);
                    }
                    else if (method == ERequestMethod.PUT)
                    {
                        response = await GetClient(authorizationToken).PutAsync(uri, content);
                    }
                    else
                    {
                        return ERequestMethod.POST.Failure(HttpStatusCode.NotImplemented, Constants.Backend.Exceptions.HTTP_VERB_KEY, this[Constants.Backend.Exceptions.HTTP_VERB_KEY]);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var outputContent = await response.Content.ReadAsStringAsync();

                        return ParseToBackendResponse(outputContent, method, ERequestResult.Success) ??
                            ERequestMethod.POST.Failure(Constants.Backend.Methods.SAVE_OR_UPDATE_KEY, HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        var outputContent = await response.Content.ReadAsStringAsync();

                        return ERequestMethod.POST.Failure(Constants.Backend.Methods.SAVE_OR_UPDATE_KEY, response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    if (e != null)
                        if (e.Message.Equals("An error occurred while sending the request"))
                            return ERequestMethod.GET.Failure(HttpStatusCode.InternalServerError, e.Source, AppResources.STATUS_CODE_INTERNAL_SERVER_ERROR);

                    return ERequestMethod.POST.Failure(HttpStatusCode.InternalServerError, e.Source, e.Message);
                }

            }
        }

        /// <summary>
        /// Method responsible for deletion operations, using HttpVerb DELETE.
        /// </summary>
        /// <param name="url">Base Url</param>
        /// <param name="query">Parameters</param>
        /// <param name="authorizationToken">Authorization Token (e.g JWT or Bearer Token)</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public async Task<BackendResponseResult> DeleteAsync(string url, string queryString = "", string authorizationToken = "")
        {
            //if (!NetworkConnectivity.CheckNetworkConnection())
			if (!CrossConnectivity.Current.IsConnected)
            {
                return ERequestMethod.DELETE.Offline();
            }
            else
            {
                try
                {
                    var uri = new Uri(string.Concat(_baseAddress, url, queryString));

                    var response = await GetClient(authorizationToken).DeleteAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var outputContent = await response.Content.ReadAsStringAsync();

                        return ParseToBackendResponse(outputContent, ERequestMethod.DELETE, ERequestResult.Success) ??
                            ERequestMethod.DELETE.Failure(Constants.Backend.Methods.DELETE_ASYNC_KEY, HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        return ERequestMethod.DELETE.Failure(Constants.Backend.Methods.DELETE_ASYNC_KEY, response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    if (e != null)
                        if (e.Message.Equals("An error occurred while sending the request"))
                            return ERequestMethod.GET.Failure(HttpStatusCode.InternalServerError, e.Source, AppResources.STATUS_CODE_INTERNAL_SERVER_ERROR);

                    return ERequestMethod.DELETE.Failure(HttpStatusCode.InternalServerError, e.Source, e.Message);
                }
            }
        }

        #endregion
    
    }
}
