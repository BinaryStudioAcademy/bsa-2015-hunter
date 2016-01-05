using Hunter.Services.Rest.Intf;
using SOS.Mobile.Common.Server.NetworkErrors;

namespace Hunter.Services.Rest.NetworkErrors.ErrorHandlers
{
    public interface IDefaultNetworkErrorHandler
    {
        NetworkError Error { get; }

        void Handle(NetworkErrorEventArgs exception);
    }
}