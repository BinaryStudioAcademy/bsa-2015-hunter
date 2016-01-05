using System;
using System.Net;
using Hunter.Common.Concrete;
using Hunter.Services.Rest.Intf;
using Hunter.Services.Rest.NetworkErrors.ErrorHandlers;

namespace SOS.Mobile.Common.Server.NetworkErrors.ErrorHandlers
{
    public class CustomServerErrorHandler : BaseNetworkErrorHandler
    {
        public override NetworkError Error { get { return NetworkError.InternalServerError; } }


        public override bool CanHandle(HttpRequestException exception)
        {
            return true;
            //return exception.Response != null && exception.Response.StatusCode == HttpStatusCode.InternalServerError;
        }

        public override void Handle(NetworkErrorEventArgs args)
        {
            Logger.Instance.Log("CustomServerErrorHandler: {0}", args.Exception);
        }
    }
}
