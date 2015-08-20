using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("SpecialNote")]
    public partial class SpecialNote : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(3000)]
        public string Text { get; set; }

        public DateTime LastEdited { get; set; }

        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        public int? UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
