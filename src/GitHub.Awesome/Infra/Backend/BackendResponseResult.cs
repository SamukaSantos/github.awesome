
using System.Collections.Generic;
using System.Net;

namespace GitHub.Awesome.Infra.Backend
{
    /// <summary>
    /// Transport class, containing response from current request.
    /// </summary>
    public class BackendResponseResult
    {
        public string Method { get; set; }
        public string Result { get; set; }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IList<RequestResultErrorItem> Errors { get; set; }
    }
}
