using System;
using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites.Enums;

namespace Hunter.Services
{
    public static class ActivityExtension
    {
        public static ActivityDto ToActivityDto(this Activity activity)
        {
            ActivityDto activityDto = new ActivityDto()
            {
                Id = activity.Id.ToString(),
                Message = activity.Message,
                Tag = activity.Tag,
                UserLogin = activity.UserLogin,
                Url = activity.Url,
                Time = activity.Time
            };

            return activityDto;
        }

        public static Activity ToActivity(this ActivityDto activityDto)
        {
            Activity activity = new Activity()
            {
                Id = Convert.ToInt32(activityDto.Id),
                Message = activityDto.Message,
                Tag = activityDto.Tag,
                UserLogin = activityDto.UserLogin,
                Url = activityDto.Url,
                Time = activityDto.Time
            };

            return activity;
        }
    }
}
