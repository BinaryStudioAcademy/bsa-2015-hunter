using System.Net.Http;
using Hunter.Services.Rest.Intf;

namespace Hunter.Services.Rest
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(HttpClientHandler httpHandler)
        {
            return new HttpClient(httpHandler);
        }
    }
}