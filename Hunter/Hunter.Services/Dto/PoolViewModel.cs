using System.ComponentModel.DataAnnotations;

namespace Hunter.Services.Dto
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