using Hunter.Services.Rest.Intf;
using SOS.Mobile.Common.Server.NetworkErrors;

namespace Hunter.Services.Rest.NetworkErrors.ErrorHandlers
{
    public abstract class BaseNetworkErrorHandler
    {
        public abstract NetworkError Error { get; }

        public abstract bool CanHandle(HttpRequestException exception);

        public abstract void Handle(NetworkErrorEventArgs exception);
    }
}