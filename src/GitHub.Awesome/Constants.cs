
namespace GitHub.Awesome
{
    public class Constants
    {
        /// <summary>
        /// Constants for Caching representation.
        /// </summary>
		public class Caching
		{
			public static string Repository  = "repository_key";
			public static string PullRequest = "pull_request_key";
			public static string Paging      = "paging_key";

		}

		/// <summary>
        /// Constants for pull request state representation.
        /// </summary>
        public class PullRequestState
        {
            public static string Open = "open";
        }

        /// <summary>
        /// Constants for Backend representation.
        /// </summary>
		public class Backend
        {
            public class Methods
            {
                public static string AUTHORIZATION_KEY  = "Authorization";
                public static string GET_DATA_ASYNC_KEY = "get_data_async_key";
                public static string SAVE_OR_UPDATE_KEY = "save_or_update_async_key";
                public static string DELETE_ASYNC_KEY   = "delete_async_key";
            }

            public class Exceptions
            {
                public static string HTTP_VERB_KEY = "http_verb_key";
            }

			public class ServiceApi
			{
				public static string UserAgent       = "http://developer.github.com/v3/#user-agent-required";

				public static string Repository      = "search/repositories";
				public static string RepositoryQuery = "?q=language:JavaScript&sort=stars&page={0}&per_page={1}";

                public static string Pull = "/repos/freeCodeCamp/freeCodeCamp/pulls";
            }
        }      
    }
}
