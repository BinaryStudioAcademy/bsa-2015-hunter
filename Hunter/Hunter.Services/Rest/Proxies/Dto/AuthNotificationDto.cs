using System.Collections.Generic;

namespace Hunter.Services.Rest.Proxies.Dto
{
    public class AuthNotificationDto
    {
        public string title { get; set; }
        public string text { get; set; }
        public List<string> images { get; set; }
        public string url { get; set; }
        public string sound { get; set; }
        public string serviceType { get; set; }
        public List<string> users { get; set; }
    }
}