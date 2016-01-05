using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Concrete;
using Hunter.Services.Rest.Intf;
using Newtonsoft.Json;
using SOS.Mobile.Common.Server.NetworkErrors;
using HttpRequestException = SOS.Mobile.Common.Server.NetworkErrors.HttpRequestException;

namespace Hunter.Services.Rest
{
    public class RestRequest : IRestRequest
    {
        protected const string AuthTokenHeader = "x-access-token";
        private readonly IHttpClientFactory _httpFactory;
        private readonly NetworkErrorFacade _errorFacade;
        private object _requestData;
        private readonly IDictionary<string, string> _requestParams = new Dictionary<string, string>();
        private readonly IDictionary<string, IEnumerable<string>> _requestHeaders = new Dictionary<string, IEnumerable<string>>();
        private readonly IList<string> _requestPathParts = new List<string>();

        private string logTag
        {
            get
            {
                return string.Format("RestRequest {0} '{1}'", HttpMethod, Url);
            }
        }

        /// <summary>
        /// Gets or sets the REST service URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method (GET, POST, PUT, DELETE).
        /// </summary>
        /// <value>
        /// The HTTP method.
        /// </value>
        public HttpMethod HttpMethod { get; set; }

        public object Data
        {
            get { return _requestData; }
            set { _requestData = value; }
        }
        public IDictionary<string, string> Params { get { return _requestParams; } }

        public IDictionary<string, IEnumerable<string>> Headers { get { return _requestHeaders; } }

		public bool IncludeAuthToken { get; set; }
        public Cookie Cookie { get; set; }

        public IList<string> PathParts { get { return _requestPathParts; } }

        public event EventHandler<NetworkErrorEventArgs> NetworkError;

        public RestRequest(NetworkErrorFacade errorFacade,
            IHttpClientFactory httpFactory)
        {
            _errorFacade = errorFacade;
            _httpFactory = httpFactory;
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>()
        {
            HttpResponseMessage response = null;
            bool error = false;
            try
            {
                var retry = false;
                var retryNum = 2;
                do
                {
                    error = false;
                    var request = prepareRequest();
                    var cookieContainer = new CookieContainer();
                    using (var handler = new HttpClientHandler() {CookieContainer = cookieContainer})
                    {
                        using (var httpClient =
                                _httpFactory.CreateClient(handler))
                        {
                            if (Cookie != null)
                            {
                                cookieContainer.Add(request.RequestUri, Cookie);
                            }
                            //if (request.Method.Method.ToLower() == "post")
                            //{
                            //    cookieContainer.Add(request.RequestUri,
                            //        new Cookie("x-access-token", HttpContextExtensions.GetTokenFromRequest()));
                            //}
                            try
                            {
                                response = await httpClient.SendAsync(request).ConfigureAwait(false);
                                response.EnsureSuccessStatusCode();

                                //OnReceivedHash(response);
                                var content = await response.Content.ReadAsStringAsync();
                                var obj = JsonConvert.DeserializeObject<T>(content);
                                return new RestResponse<T>(obj, response.StatusCode);
                            }
                            catch (Exception ex)
                            {
                                error = true;
                                retry = OnError(new HttpRequestException(response, ex), new Uri(Url));
                            }
                        }
                    }
                } while (retry && --retryNum > 0);
            }
            catch (Exception exception)
            {
                error = true;
                Logger.Instance.Log(string.Format("{0}: ????? Unexpected exception :{1}", logTag, exception));
            }
            finally
            {
                clearRequestParamsData();
                if (response != null)
                {
                    response.Dispose();
                }
            }

            return new RestResponse<T>(default(T), response != null ? (HttpStatusCode?)response.StatusCode : null);
        }

        protected virtual StringContent ToJsonContent(object payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        private void clearRequestParamsData()
        {
            _requestPathParts.Clear();
            _requestParams.Clear();
            _requestData = null;
        }

        private HttpRequestMessage prepareRequest()
        {
            var requestUrl = Url.TrimEnd(/*'/', */' ');
            var serializer = new HttpParamsSerializer();

            if (_requestPathParts.Count > 0)
            {
                var parts = serializer.Serialize(_requestPathParts);
                requestUrl += parts;
            }

            HttpRequestMessage request;
            if (HttpMethod == System.Net.Http.HttpMethod.Get)
            {
                string content = "";
                if (_requestParams.Count > 0)
                {
                    var query = serializer.Serialize(_requestParams);
                    content = query;
                }
                if (_requestData != null)
                {
                    var query = serializer.Serialize(_requestData);
                    content = content + query;
                }
                requestUrl += content;
                request = new HttpRequestMessage(HttpMethod, requestUrl);
            }
            else
            {
                if (_requestParams.Count > 0 && _requestData != null)
                {
                    throw new InvalidOperationException("Use either SetData or SetParam to set body of request");
                }

                request = new HttpRequestMessage(HttpMethod, requestUrl);
                if (_requestParams.Count > 0 || _requestData != null)
                {
                    request.Content = ToJsonContent(_requestData ?? _requestParams); ;
                }
            }

            if (_requestHeaders.Count > 0)
            {
                foreach (var header in _requestHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (IncludeAuthToken)
            {
                request.Headers.Add(AuthTokenHeader, HttpContextExtensions.GetTokenFromRequest());
            }

            return request;
        }

        protected bool OnError(HttpRequestException exception, Uri requestUri)
        {
            var customHandler = NetworkError;
            var args = new NetworkErrorEventArgs(exception, requestUri);
            if (customHandler != null)
            {
                args.Error = _errorFacade.ResolveError(args);
                customHandler(this, args);
            }
            if (!args.Handled)
            {
                _errorFacade.Handle(args);
            }
            if (!args.Handled)
            {
                Logger.Instance.Log(string.Format("{0}: Request to {1} failed", Url, logTag));
            }
            return args.Handled && args.Retry;
        }
    }
}