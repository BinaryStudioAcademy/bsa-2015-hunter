using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;

namespace Hunter.Services.Extensions
{
    public static class ScheduledNotificationExtension
    {
        public static ScheduledNotificationDto ToScheduledNotificationDto(this ScheduledNotification notification)
        {
            var s = new ScheduledNotificationDto
            {
                Id = notification.Id,
                CandidateId = notification.CandidateId,
                Message = notification.Message,
                Pending = notification.Pending,
                UserProfileId = notification.UserProfileId,
                IsSent = notification.IsSent,
                IsShown = notification.IsShown,
                UserLogin = notification.UserProfile != null ? notification.UserProfile.Alias : string.Empty
            };
            return s;
        }

        public static ScheduledNotification ToScheduledNotification(this ScheduledNotificationDto notificationDto)
        {
            var notification = new ScheduledNotification
            {
                Pending = notificationDto.Pending,
                Message = notificationDto.Message,
                IsSent = notificationDto.IsSent,
                IsShown = notificationDto.IsShown,
                CandidateId = notificationDto.CandidateId,
                UserProfileId = notificationDto.UserProfileId
            };
            return notification;
        }

        public static void ToScheduledNotification(this ScheduledNotificationDto notificationDto,
            ScheduledNotification notification)
        {
            notification.Pending = notificationDto.Pending;
            notification.Message = notificationDto.Message;
            notification.IsSent = notificationDto.IsSent;
            notification.IsShown = notificationDto.IsShown;
            notification.CandidateId = notificationDto.CandidateId;
            notification.UserProfileId = notificationDto.UserProfileId;
        }
    }
}
