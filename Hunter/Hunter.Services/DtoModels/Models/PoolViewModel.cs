using System.ComponentModel.DataAnnotations;
using Hunter.Services.DtoModels.Extentions;

namespace Hunter.Services.DtoModels.Models
{
    public class PoolViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        // [CustomValidation(typeof(HunterValidation), "ValidateIsPoolNameExist")]
        public string Name { get; set; }
    }
}