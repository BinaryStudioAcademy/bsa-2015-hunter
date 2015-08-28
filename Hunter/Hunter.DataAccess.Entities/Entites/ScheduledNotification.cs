using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("ScheduledNotification")]
    public class ScheduledNotification : IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int UserProfileId { get; set; }
        public DateTime Pending { get; set; }
        [StringLength(2000)]
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public bool IsShown { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
