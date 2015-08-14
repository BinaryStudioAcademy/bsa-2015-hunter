using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Hunter.Services.Dto
{
    public class FileDto
    {
        public string Directory { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FileExtation { get; set; }
        public Stream File { get; set; }
    }
}
