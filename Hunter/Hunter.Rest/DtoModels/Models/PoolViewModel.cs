using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunter.Rest.DtoModels.Models
{
    public class PoolViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        //[Remote("IsPoolExists", "Pool", ErrorMessage = "Can't add what already exists!")]
        public string Name { get; set; }
    }
}