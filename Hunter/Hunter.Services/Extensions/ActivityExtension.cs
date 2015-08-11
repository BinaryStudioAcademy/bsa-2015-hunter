using System;
using System.Linq;
using Hunter.DataAccess.Entities;

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

        public static int GetAmountOfActualActivities(this IActivityService service, int lastId)
        {
            var activities = service.GetAllActivities().OrderByDescending(x => x.Time);

            int actualActivitiesAmount = activities.SkipWhile(x => Convert.ToInt32(x.Id) != lastId).Count();

            return actualActivitiesAmount;
        }
    }
}
