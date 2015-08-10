using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services
{
    public class ActivityDto
    {
        public string Id { get; set; }

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

        public DateTime Time { get; set; }
    }
}
