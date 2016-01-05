using System;
using System.Globalization;
using System.Net;
using Hunter.Common.Concrete;
using Hunter.Services.Rest.Intf;

namespace Hunter.Services.Extensions
{
    public static class RestRequestExtensions
    {
        public static IRestRequest SetParam(this IRestRequest request, string paramName, string paramValue)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentNullException("paramName");
            }
            request.Params[paramName] = paramValue;
            return request;
        }

        public static IRestRequest SetParam(this IRestRequest request, string paramName, long paramValue)
        {
            return SetParam(request, paramName, paramValue.ToString(CultureInfo.InvariantCulture));
        }

        public static IRestRequest SetPart(this IRestRequest request, string uriPart)
        {
            if (string.IsNullOrEmpty(uriPart))
                throw new ArgumentNullException("uriPart");

            request.PathParts.Add(uriPart);
            return request;
        }

        public static IRestRequest SetPart(this IRestRequest request, long uriPart)
        {
            return SetPart(request, uriPart.ToString(CultureInfo.InvariantCulture));
        }

        public static IRestRequest SetData(this IRestRequest request, object data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            request.Data = data;
            return request;
        }

        public static IRestRequest WithHeader(this IRestRequest request, string key, params string[] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (values == null)
                throw new ArgumentNullException("values");
            request.Headers.Add(key, values);
            return request;
        }

        public static IRestRequest WithAuthToken(this IRestRequest request)
        {
            request.IncludeAuthToken = true;
            return request;
        }
        public static IRestRequest WithCookie(this IRestRequest request, Cookie cookie)
        {
            request.Cookie = cookie;
            return request;
        }

        public static IRestRequest WhenError(this IRestRequest request, EventHandler<NetworkErrorEventArgs> handler)
        {
            if (handler != null)
            {
                request.NetworkError -= handler;
                request.NetworkError += handler;
            }
            return request;
        }

        public static IRestRequest LogError(this IRestRequest request, bool handle = false)
        {
            request.NetworkError += (sender, args) =>
                                    {
                                        Logger.Instance.Log("DefaultErrorLogger, : Error {0} requesting {1}",  args.Exception, args.RequestUri);
                                        args.Handled = handle;
                                    };
            return request;
        }
    }
}