using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hunter.DataAccess.Entities.Entites.Enums;

namespace Hunter.DataAccess.Entities
{
    [Table("Activity")]
    public partial class Activity : IEntity
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string UserLogin { get; set; }

        public ActivityType Tag { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        [Required]
        [StringLength(2000)]
        public string Url { get; set; }

        public DateTime Time { get; set; }
    }
}
