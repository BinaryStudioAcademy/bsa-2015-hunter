using System.Net;
using System.Threading.Tasks;
using Hunter.Services.Extensions;
using Hunter.Services.Rest.Intf;
using Hunter.Services.Rest.Proxies.Dto;

namespace Hunter.Services.Rest.Proxies
{
    public class NotificationProxy: BaseProxy
    {
        public NotificationProxy(IRestClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Informs server about emergency
        /// </summary>
        /// <param name="ivent">prepared event ot send on server</param>
        /// <returns></returns>
        public async Task<bool> SendNotification(AuthNotificationDto notification)
        {

            var res = await Post("app/api/notification").WithAuthToken()
                                .WithCookie(new Cookie("x-access-token", HttpContextExtensions.GetTokenFromRequest()))
                                             .SetData(notification)
                                             .ExecuteAsync<AuthNotificationDto>();

            return res.StatusCode != null && ((int)res.StatusCode.Value) / 100 == 2;
        }
    }
}
