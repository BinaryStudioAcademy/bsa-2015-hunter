using System;
using System.Net.Http;

namespace SOS.Mobile.Common.Server.NetworkErrors
{
    public class HttpRequestException : Exception
    {
        public HttpRequestException(string message, Exception exception) : base(message, exception)
        {
        }

        public HttpRequestException(HttpResponseMessage response, Exception exception)
            : base(exception.Message, exception)
        {
            Response = response;
        }

        public HttpResponseMessage Response { get; private set; }
    }
}
