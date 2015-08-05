using System;
using Hunter.DataAccess.Db;
using Hunter.Services.Dto;

namespace Hunter.Services.Infrastructure
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
                Url = activity.Url
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
                Url = activityDto.Url
            };

            return activity;
        }
    }
}
