using System;

namespace Hunter.Services
{
    public class ScheduledNotificationDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int UserProfileId { get; set; }
        public DateTime Pending { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public bool IsShown { get; set; }
        public string UserLogin { get; set; }
    }
}
