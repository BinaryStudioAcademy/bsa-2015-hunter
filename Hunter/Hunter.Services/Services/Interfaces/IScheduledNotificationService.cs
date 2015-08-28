﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Interfaces
{
    public interface IScheduledNotificationService
    {
        void Add(ScheduledNotificationDto notificationDto);
        void Update(ScheduledNotificationDto notificationDto);
        void Delete(int id);
        IList<ScheduledNotificationDto> Get(string userAlias);
        ScheduledNotificationDto Get(int id);
        IList<ScheduledNotificationDto> GetActive(string userLogin);
        void NotificationShown(int id);
        void Notify();
    }
}
