namespace Hunter.DataAccess.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialNote")]
    public partial class SpecialNote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserLogin { get; set; }

        [Required]
        [StringLength(3000)]
        public string Text { get; set; }

        public DateTime LastEdited { get; set; }

        public int CardId { get; set; }

        public virtual Card Card { get; set; }
    }
}
