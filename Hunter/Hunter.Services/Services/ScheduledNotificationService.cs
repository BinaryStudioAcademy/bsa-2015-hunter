using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;
using System.Net.Mail;

namespace Hunter.Services
{
    public class ScheduledNotificationService : IScheduledNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScheduledNotificationRepository _scheduledNotificationRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ScheduledNotificationService(
            IUnitOfWork unitOfWork,
            IScheduledNotificationRepository scheduledNotificationRepository,
            IUserProfileRepository userProfileRepository
            )
        {
            _unitOfWork = unitOfWork;
            _scheduledNotificationRepository = scheduledNotificationRepository;
            _userProfileRepository = userProfileRepository;
        }

        public void Add(ScheduledNotificationDto notificationDto)
        {
            var notification = notificationDto.ToScheduledNotification();
            _scheduledNotificationRepository.UpdateAndCommit(notification);
        }

        public void Update(ScheduledNotificationDto notificationDto)
        {
            var notification = _scheduledNotificationRepository.Get(notificationDto.Id);
            if (notification != null)
            {
                notificationDto.ToScheduledNotification(notification);
                _scheduledNotificationRepository.UpdateAndCommit(notification);
            }
        }

        public void Delete(int id)
        {
            var notification = _scheduledNotificationRepository.Get(id);
            if (notification != null)
                _scheduledNotificationRepository.DeleteAndCommit(notification);
        }

        public IList<ScheduledNotificationDto> Get(string userLogin)
        {
            var userProfile = _userProfileRepository.Get(p => p.UserLogin == userLogin);
            var notifications = _scheduledNotificationRepository.QueryIncluding(n => n.UserProfileId == userProfile.Id).ToList();
            return notifications.Select(item => item.ToScheduledNotificationDto()).ToList();
        }

        public ScheduledNotificationDto Get(int id)
        {
            var vacancy = _scheduledNotificationRepository.Get(id);
            if (vacancy != null)
                return vacancy.ToScheduledNotificationDto();
            return null;
        }

        public void Notify()
        {
            throw new NotImplementedException();
            var pendingDate = DateTime.UtcNow.Date.AddDays(1);
            var notifications = _scheduledNotificationRepository.QueryIncluding(n => n.Pending < pendingDate && !n.IsSent).OrderBy(n => n.UserProfileId).ToList();
            var currentEmail = string.Empty;
            var emailClient = new SmtpClient();
            MailMessage mailMessage = null;
            foreach (var n in notifications)
            {
                if (currentEmail != n.UserProfile.UserLogin)
                {
                    if (currentEmail != string.Empty)
                    {
                        emailClient.Send(mailMessage);
                    }
                    currentEmail = n.UserProfile.UserLogin;
                    mailMessage = new MailMessage("Hunter App", currentEmail);
                    emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                }
            }
        }
    }
}