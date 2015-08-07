using System.Collections.Generic;

namespace Hunter.Tools.LinkedIn
{
    public class PublicPageInfo
    {
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }
        public string Summary { get; set; }
        public IEnumerable<string> Skills { get; set; }
        public string Img { get; set; }
        public string ExperienceTime { get; set; }
        public IEnumerable<string> Experience { get; set; }
        public IEnumerable<string> Courses { get; set; }
        public IEnumerable<string> Projects { get; set; }
        public IEnumerable<string> Certifications { get; set; }
        public string Languages { get; set; }
        public IEnumerable<string> Education { get; set; }
    }
}
