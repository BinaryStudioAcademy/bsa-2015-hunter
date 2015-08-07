using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hunter.Services
{
    public class PoolViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        // [CustomValidation(typeof(HunterValidation), "ValidateIsPoolNameExist")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}