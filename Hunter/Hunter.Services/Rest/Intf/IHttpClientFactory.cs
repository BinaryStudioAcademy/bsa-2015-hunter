using System.Net.Http;

namespace Hunter.Services.Rest.Intf
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient(HttpClientHandler httpHandler);
    }
}
