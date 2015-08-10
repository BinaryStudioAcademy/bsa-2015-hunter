using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
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
    }
}
