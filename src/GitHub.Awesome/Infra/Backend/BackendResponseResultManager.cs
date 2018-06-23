
using GitHub.Awesome.Infra.Backend.Enum;
using GitHub.Awesome.Infra.Backend.Exceptions;
using GitHub.Awesome.Infra.Resources;
using System.Collections.Generic;
using System.Net;

namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Helper class to provide a defined BackendResponseResult.
    /// Creation methods represent possible failures.
    /// </summary>
    public static class BackendResponseResultManager
    {
        /// <summary>
        /// Creates representation of Offline state.
        /// </summary>
        /// <param name="method">HttpVerb.</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public static BackendResponseResult Offline(this ERequestMethod method)
        {
            var offlineException = new OffLineException();

            return new BackendResponseResult()
            {
                Method = method.ToString(),
                Result = ERequestResult.Error.ToString(),
                StatusCode = HttpStatusCode.ServiceUnavailable,
                Content = null,
                Errors = new List<RequestResultErrorItem>
                {
                    new RequestResultErrorItem
                    {
                        Key = "Offline",
                        Message = offlineException.Message
                    }
                }
            };
        }

        /// <summary>
        /// Creates representation of Invalid Token state.
        /// </summary>
        /// <param name="method"></param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public static BackendResponseResult InvalidToken(this ERequestMethod method)
        {
            return new BackendResponseResult()
            {
                Method = method.ToString(),
                Result = ERequestResult.Error.ToString(),
                StatusCode = HttpStatusCode.Forbidden,
                Content = null,
                Errors = null
            };
        }

        /// <summary>
        /// Creates representation of generic failure.
        /// </summary>
        /// <param name="method">HttpVerb.</param>
        /// <param name="key">Error Key (Representation of key in Notifiable object).</param>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public static BackendResponseResult Failure(this ERequestMethod method, string key, HttpStatusCode statusCode)
        {
            return new BackendResponseResult()
            {
                Method = method.ToString(),
                Result = ERequestResult.Error.ToString(),
                Content = null,
                StatusCode = statusCode,
                Errors = new List<RequestResultErrorItem>
                {
                    new RequestResultErrorItem
                    {
                        Key = key,
                        Message = HandleStatusCodeResponse(statusCode)
                    }
                }
            };
        }

        /// <summary>
        /// Creates representation of generic failure.
        /// </summary>
        /// <param name="method">HttpVerb</param>
        /// <param name="statusCode">Http statuc code.</param>
        /// <param name="key">Error Key (Representation of key in Notifiable object).</param>
        /// <param name="error">Error message.</param>
        /// <returns>Wrappered BackendResponseResult.</returns>
        public static BackendResponseResult Failure(this ERequestMethod method, HttpStatusCode statusCode, string key, string error = null)
        {
            return new BackendResponseResult()
            {
                Method = method.ToString(),
                Result = ERequestResult.Error.ToString(),
                StatusCode = statusCode,
                Content = null,
                Errors = new List<RequestResultErrorItem>
                {
                    new RequestResultErrorItem
                    {
                        Key = key,
                        Message = error
                    }
                }
            };
        }

        /// <summary>
        /// Returns specific message based on HttpStatusCode.
        /// </summary>
        /// <param name="code">HttpStatusCode.</param>
        /// <returns>Error Message.<returns>
        private static string HandleStatusCodeResponse(HttpStatusCode code)
        {

            switch (code)
            {
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Ambiguous:
                case HttpStatusCode.BadGateway:
                    return string.Empty;
                case HttpStatusCode.BadRequest:
                    return AppResources.STATUS_CODE_BAD_REQUEST;
                case HttpStatusCode.Conflict:
                case HttpStatusCode.Continue:
                case HttpStatusCode.Created:
                case HttpStatusCode.ExpectationFailed:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Found:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.Gone:
                case HttpStatusCode.HttpVersionNotSupported:
                    return string.Empty;
                case HttpStatusCode.InternalServerError:
                    return AppResources.STATUS_CODE_INTERNAL_SERVER_ERROR;
                case HttpStatusCode.LengthRequired:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.Moved:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NonAuthoritativeInformation:
                case HttpStatusCode.NotAcceptable:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.NotModified:
                case HttpStatusCode.OK:
                case HttpStatusCode.PartialContent:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.PreconditionFailed:
                case HttpStatusCode.ProxyAuthenticationRequired:
                case HttpStatusCode.RedirectKeepVerb:
                case HttpStatusCode.RedirectMethod:
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                case HttpStatusCode.RequestEntityTooLarge:
                case HttpStatusCode.RequestTimeout:
                case HttpStatusCode.RequestUriTooLong:
                case HttpStatusCode.ResetContent:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.SwitchingProtocols:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.UnsupportedMediaType:
                case HttpStatusCode.Unused:
                case HttpStatusCode.UpgradeRequired:
                case HttpStatusCode.UseProxy:
                default:
                    return string.Empty;
            }

        }
    }
}
