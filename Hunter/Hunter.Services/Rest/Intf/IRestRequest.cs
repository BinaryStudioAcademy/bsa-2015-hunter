using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SOS.Mobile.Common.Server;
using SOS.Mobile.Common.Server.NetworkErrors;

namespace Hunter.Services.Rest.Intf
{

    /// <summary>
    /// The verbs which are valid for the REST services.
    /// </summary>
    public class RestVerbs
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Delete = "DELETE";
    }

    /// <summary>
    /// The interface of the request for sending to the REST service.
    /// </summary>
    public interface IRestRequest
    {
        /// <summary>
        /// Gets or sets the REST service URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method (GET, POST, PUT, DELETE).
        /// </summary>
        /// <value>
        /// The HTTP method.
        /// </value>
        System.Net.Http.HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// Contains the data that will be sent in the request HTTP query parameters, i.e. host/users/get?user=12.
        /// </summary>
        IDictionary<string, string> Params { get; }

        /// <summary>
        /// Contains the data that will be sent in the request HTTP query parameters, i.e. host/users/get?user=12.
        /// </summary>
        IDictionary<string, IEnumerable<string>> Headers { get; }

        /// <summary>
        /// If true, auth token should be included in current server request
        /// </summary>
        bool IncludeAuthToken { get; set; }

        /// <summary>
        /// If not empty, request sends with cookie
        /// </summary>
        Cookie Cookie { get; set; }

        /// <summary>
        /// Contains the data that will be sent as part of url, i.e. host/users/get/12.
        /// </summary>
        IList<string> PathParts { get; }

        /// <summary>
        /// Contains the complex object that will be sent in the request body.
        /// </summary>
        object Data { get; set; }
        
        event EventHandler<NetworkErrorEventArgs> NetworkError;

        Task<RestResponse<T>> ExecuteAsync<T>();
    }

    public class NetworkErrorEventArgs : EventArgs
    {
        public HttpRequestException Exception { get; private set; }
        public bool Handled { get; set; }
        public bool Retry { get; set; }
        public NetworkError Error { get; set; }
        public Uri RequestUri { get; private set; }

        public NetworkErrorEventArgs(HttpRequestException exception, Uri requestUri, bool handled = false)
        {
            Exception = exception;
            RequestUri = requestUri;
            Handled = handled;
        }
    }
}