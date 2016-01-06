using System;
using System.Collections.Generic;
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
using Hunter.Services.Services.Interfaces;
using Newtonsoft.Json;

namespace Hunter.Services
{
    public class ActivityPostService : IActivityPostService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserProfileAuthenticationService _profileAuthenticationService;
        private readonly NotificationProxy _notificationProxy;

        public ActivityPostService(IActivityRepository activityRepository, IUserProfileAuthenticationService profileAuthenticationService, NotificationProxy notificationProxy)
        {
            _activityRepository = activityRepository;
            _profileAuthenticationService = profileAuthenticationService;
            _notificationProxy = notificationProxy;
        }

        public async Task Post(string message, ActivityType tag, Uri url = null)
        {
            var activity = new Activity
            {
                Message = message,
                Tag = tag,
                Url = url != null ? url.ToString() : null,
                UserProfileId = _profileAuthenticationService.GetUserIdByUserLogin(Thread.CurrentPrincipal.Identity.Name),
                //_userProfileRepository.Get(x => x.UserLogin == Thread.CurrentPrincipal.Identity.Name).Id,
                Time = DateTime.UtcNow
            };
            _activityRepository.UpdateAndCommit(activity);
            var authNotificationDto = activity.ToAuthNotificationDto();
            authNotificationDto.users = _profileAuthenticationService.GetAuthUserIdByRole(Roles.Recruiter.ToString(), Thread.CurrentPrincipal.Identity.Name).ToList();
            var resp1 = await _notificationProxy.SendNotification(authNotificationDto);
            
        }

    }
}