using Hunter.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Hunter.DataAccess.Db
{
    [Table("SpecialNote")]
    public partial class SpecialNote : IEntity
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
