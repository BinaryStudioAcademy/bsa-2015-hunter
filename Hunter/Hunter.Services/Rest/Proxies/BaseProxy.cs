using System;
using Hunter.Services.Extensions;
using Hunter.Services.Rest.Intf;

namespace Hunter.Services.Rest.Proxies
{
    public class BaseProxy
    {

        private const string _ApiKeyHeader = "S-Api-Key";
        private string _sApiKey = "xyz";

        private readonly IRestClient _rest;
        
        public string BaseAddress { get; protected set; }


        public BaseProxy(IRestClient client)
        {
            _rest = client;
            BaseAddress = "http://team.binary-studio.com/";
        }

        protected IRestRequest Post(string relativeUrl)
        {
            var absolute = getAddress(relativeUrl);
            return _rest.Post(absolute);//.WithHeader(_ApiKeyHeader, _sApiKey);
        }

        protected IRestRequest Get(string baseUrl, string relativeUrl)
        {
            var absolute = createAbsoluteUrl(relativeUrl, baseUrl);
            return _rest.Get(absolute);//.WithHeader(_ApiKeyHeader, _sApiKey);
        }

        protected IRestRequest Get(string relativeUrl)
        {
            var absolute = getAddress(relativeUrl);
            return _rest.Get(absolute);
        }

        protected IRestRequest Put(string relativeUrl)
        {
            var absolute = getAddress(relativeUrl);
            return _rest.Put(absolute);//.WithHeader(_ApiKeyHeader, _sApiKey);
        }
        
        private string getAddress(string relativeUrl)
        {
            return createAbsoluteUrl(relativeUrl, BaseAddress);
        }

        private static string createAbsoluteUrl(string relativePath, string baseAddress = null)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new InvalidOperationException("BaseUrl is empty.");
            }
            baseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";
            return baseAddress + relativePath.TrimStart('/');
        }
    }
}