using System;
using System.Threading;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class ActivityPostService : IActivityPostService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityPostService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void Post(string message, string tag, Uri url = null)
        {
            var activity = new Activity
            {
                Message = message,
                Tag = tag,
                Url = url != null ? url.ToString() : null,
                UserLogin = Thread.CurrentPrincipal.Identity.Name,
                Time = DateTime.UtcNow
            };
            _activityRepository.UpdateAndCommit(activity);
        }
    }
}