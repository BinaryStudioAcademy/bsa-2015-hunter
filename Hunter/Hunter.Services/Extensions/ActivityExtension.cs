using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class ActivityExtension
    {
        public static ActivityDto ToActivityDto(this Activity activity)
        {
            ActivityDto activityDto = new ActivityDto()
            {
                Id = activity.Id,
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
            return new Activity()
            {
                Id = activityDto.Id,
                Message = activityDto.Message,
                Tag = activityDto.Tag,
                UserLogin = activityDto.UserLogin,
                Url = activityDto.Url,
                Time = activityDto.Time
            };
        }
    }
}
