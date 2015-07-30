namespace Hunter.DataAccess.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Activity")]
    public partial class Activity
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string UserLogin { get; set; }

        [StringLength(150)]
        public string Tag { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        [Required]
        [StringLength(2000)]
        public string Url { get; set; }
    }
}
