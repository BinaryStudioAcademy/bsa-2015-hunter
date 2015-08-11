using System.Web;
using Newtonsoft.Json;

namespace Hunter.Services.Dto
{
    public class FileDto
    {
        [JsonProperty]
        public string Directory { get; set; }
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Email { get; set; }
        public HttpPostedFile File { get; set; }
    }
}
