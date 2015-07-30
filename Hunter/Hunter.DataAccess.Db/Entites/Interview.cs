namespace Hunter.DataAccess.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Interview")]
    public partial class Interview
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
