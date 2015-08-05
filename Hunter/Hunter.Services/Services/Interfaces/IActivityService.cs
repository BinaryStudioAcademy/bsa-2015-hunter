using System.Collections.Generic;

namespace Hunter.Services
{
    public interface IActivityService
    {
        IEnumerable<ActivityDto> GetAllActivities();
        ActivityDto GetActivityById(int id);
        void AddActivity(ActivityDto entitiy);
        void UpdateActivity(ActivityDto entity);
        void DeleteActivityById(int id);
    }
}
