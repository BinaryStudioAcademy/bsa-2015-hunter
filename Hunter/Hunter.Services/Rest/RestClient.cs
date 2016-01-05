using System;
using System.Web;
using Hunter.Services.Rest.Intf;
using SOS.Mobile.Common.Server;
using SOS.Mobile.Common.Server.NetworkErrors;
using SOS.Mobile.Common.Server.NetworkErrors.ErrorHandlers;

namespace Hunter.Services.Rest
{
    /// <summary>
    /// The client for accessing the REST service.
    /// </summary>
    /// <example>
    /// var client = new RestClient();
    /// client.Post("www.rest-server.com/api").SetBodyData(data).OnError(Logger.Log).Execute();
    /// </example>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Creates GET request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="RestRequest"/>.</returns>
        public IRestRequest Get(string url)
        {
            return CreateRequest(url, System.Net.Http.HttpMethod.Get);
        }

        /// <summary>
        /// Creates POST request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="RestRequest"/>.</returns>
        public IRestRequest Post(string url)
        {
            return CreateRequest(url, System.Net.Http.HttpMethod.Post);
        }

        /// <summary>
        /// Creates PUT request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="RestRequest"/>.</returns>
        public IRestRequest Put(string url)
        {
            return CreateRequest(url, System.Net.Http.HttpMethod.Put);
        }

        /// <summary>
        /// Creates DELETE request.
        /// </summary>
        /// <param name="url">The URL of the REST service.</param>
        /// <returns>The instance of the <see cref="RestRequest"/>.</returns>
        public IRestRequest Delete(string url)
        {
            return CreateRequest(url, System.Net.Http.HttpMethod.Delete);
        }

        protected virtual IRestRequest CreateRequest(string url, System.Net.Http.HttpMethod httpMethod)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url", "The REST service URL should be specified");

            IRestRequest restRequest = new RestRequest(new NetworkErrorFacade(new CustomServerErrorHandler()),  new HttpClientFactory());
            restRequest.Url = url;
            restRequest.HttpMethod = httpMethod;

            return restRequest;
        }
    }
    public static class HttpContextExtensions
    {
        public static string GetTokenFromRequest()
        {
            var httpCookie = HttpContext.Current.Request.Cookies.Get("x-access-token");
            if (httpCookie == null) return null;
            var token = httpCookie.Value;
            return token;
        }
    }
}
