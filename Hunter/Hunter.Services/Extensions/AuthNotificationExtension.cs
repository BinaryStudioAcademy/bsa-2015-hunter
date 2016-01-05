using System.Collections.Generic;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;
using Hunter.Services.Rest.Proxies.Dto;

namespace Hunter.Services.Extensions
{
    public static class AuthNotificationExtension
    {
        public static AuthNotificationDto ToAuthNotificationDto(this Activity activity)
        {
            AuthNotificationDto activityDto = new AuthNotificationDto()
            {
                        title = activity.Tag.ToString(),
                        text = activity.Message,
                        serviceType = "HR Hunter",
                        url = activity.Url,
                        //TODO delete this hardcode
                        users = new List<string> { "567bb126cd2307e21e6cd333"},
                        sound = true.ToString()
    };

            return activityDto;
        }
    }
}
