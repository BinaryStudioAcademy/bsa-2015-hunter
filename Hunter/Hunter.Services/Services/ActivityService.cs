using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ActivityService(IActivityRepository activityRepository, IUserProfileRepository userProfileRepository)
        {
            _activityRepository = activityRepository;
            _userProfileRepository = userProfileRepository;
        }

        public IEnumerable<ActivityDto> GetAllActivities()
        {
            var activities = _activityRepository.All();

            return activities.Select(item => item.ToActivityDto()).ToList();
        }


        public ActivityDto GetActivityById(int id)
        {
            return _activityRepository.Get(id).ToActivityDto();
        }


        public void AddActivity(ActivityDto entity)
        {
            _activityRepository.UpdateAndCommit(entity.ToActivity());
        }

        public void UpdateActivity(ActivityDto entity)
        {
            _activityRepository.UpdateAndCommit(entity.ToActivity());
        }


        public void DeleteActivityById(int id)
        {
            var activity = _activityRepository.Get(id);
            _activityRepository.DeleteAndCommit(activity);
        }

        public int GetUnreadActivitiesForUser(string login)
        {
            var user = _userProfileRepository.Get(login);

            return _activityRepository.GetCountOfActivitiesSince(user.LastViewedActivityId);
        }

        public void UpdateLastSeenActivity(string login, int lastSeenActivityId)
        {
            var userProfile = _userProfileRepository.Get(login);
            if (userProfile != null && userProfile.LastViewedActivityId < lastSeenActivityId)
            {
                userProfile.LastViewedActivityId = lastSeenActivityId;
                _userProfileRepository.UpdateAndCommit(userProfile);
            }
        }
    }
}
