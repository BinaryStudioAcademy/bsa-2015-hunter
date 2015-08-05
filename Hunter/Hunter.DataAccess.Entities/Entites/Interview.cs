using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("Interview")]
    public partial class Interview : IEntity
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public DateTime StartTime { get; set; }

        [StringLength(10)]
        public string Comments { get; set; }

        [StringLength(10)]
        public string FeedbackId { get; set; }

        public virtual Card Card { get; set; }
    }
}
