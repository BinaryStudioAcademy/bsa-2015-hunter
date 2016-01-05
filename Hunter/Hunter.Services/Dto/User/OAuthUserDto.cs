using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hunter.Services.Dto.User
{
    public class OAuthUserDto
    {
        [JsonProperty(PropertyName = "_id")]
        public string AuthUserId { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
