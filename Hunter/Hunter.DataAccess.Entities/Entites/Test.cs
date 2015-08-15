using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("Test")]
    public partial class Test : IEntity
    {
        public int Id { get; set; }

        [StringLength(2000)]
        public string Url { get; set; }

        public int? FileId { get; set; }


        [StringLength(3000)]
        public string Comment { get; set; }

        public int CardId { get; set; }

        public int? FeedbackId { get; set; }

        public virtual Card Card { get; set; }

        public virtual File File { get; set; }

        public virtual Feedback Feedback { get; set; }
    }
}
