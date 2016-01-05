using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Services.Rest.Intf;
using Hunter.Services.Rest.NetworkErrors.ErrorHandlers;
using SOS.Mobile.Common.Server.NetworkErrors.ErrorHandlers;

namespace SOS.Mobile.Common.Server.NetworkErrors
{
    public class NetworkErrorFacade
    {
        private readonly List<BaseNetworkErrorHandler> _errorHandlers;

        protected BaseNetworkErrorHandler UnknownErrorHandler
        {
            get
            {
                var handler = _errorHandlers.FirstOrDefault(h => h.Error == NetworkError.Unknown);
                if (handler == null)
                    throw new InvalidOperationException("At least 1 handler of NetworkError.Unknown should be registered");
                return handler;
            }
        }

        public NetworkErrorFacade(
            //UnauthorizedErrorHandler unauthorizedHandler,
            //NoConnectionErrorHandler noConnectionHandler,
            CustomServerErrorHandler internalServerErrorHandler
            //UnknownErrorHandler unknownErrorHandler
            )
        {
            _errorHandlers = new List<BaseNetworkErrorHandler>
                             {
                                 //unauthorizedHandler,
                                 //noConnectionHandler,
                                 internalServerErrorHandler,
                                 //unknownErrorHandler,
                             };
        }

        public NetworkError ResolveError(NetworkErrorEventArgs errorArgs)
        {
            return getHandler(errorArgs.Exception).Error;
        }

        public void Handle(NetworkErrorEventArgs errorArgs)
        {
            getHandler(errorArgs.Exception).Handle(errorArgs);
        }

        private BaseNetworkErrorHandler getHandler(HttpRequestException error)
        {
            var handler = _errorHandlers.SingleOrDefault(h => h.CanHandle(error));
            return handler ?? UnknownErrorHandler;
        }
    }
}