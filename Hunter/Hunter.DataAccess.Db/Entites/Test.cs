namespace Hunter.DataAccess.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Test")]
    public partial class Test
    {
        public int Id { get; set; }

        [StringLength(2000)]
        public string Url { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public byte[] FileStream { get; set; }

        [StringLength(3000)]
        public string Comment { get; set; }

        public int CardId { get; set; }

        public int? FeedbackId { get; set; }

        public virtual Card Card { get; set; }

        public virtual Feedback Feedback { get; set; }
    }
}
