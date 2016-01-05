using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto.User;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;
using Hunter.Services.Rest;
using Hunter.Services.Rest.Proxies;
using Hunter.Services.Rest.Proxies.Dto;
using Newtonsoft.Json;

namespace Hunter.Services
{
    public class ActivityPostService : IActivityPostService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        //private readonly IUserProfileService _userProfileService;
        private readonly NotificationProxy _notificationProxy;

        public ActivityPostService(IActivityRepository activityRepository, IUserProfileRepository userProfileRepository,/*IUserProfileService userProfileService,*/ NotificationProxy notificationProxy)
        {
            _activityRepository = activityRepository;
            _userProfileRepository = userProfileRepository;
            //_userProfileService = userProfileService;
            _notificationProxy = notificationProxy;
        }

        public async Task Post(string message, ActivityType tag, Uri url = null)
        {
            var activity = new Activity
            {
                Message = message,
                Tag = tag,
                Url = url != null ? url.ToString() : null,
                UserProfileId = _userProfileRepository.Get(x => x.UserLogin == Thread.CurrentPrincipal.Identity.Name).Id,
                Time = DateTime.UtcNow
            };
            var authNotificationDto = activity.ToAuthNotificationDto();
            //authNotificationDto.users = _userProfileService.GetAuthUserIdByRole(Roles.Recruiter.ToString()).ToList();


            var resp1 = await _notificationProxy.SendNotification(authNotificationDto);
            _activityRepository.UpdateAndCommit(activity);
        }


    }
}