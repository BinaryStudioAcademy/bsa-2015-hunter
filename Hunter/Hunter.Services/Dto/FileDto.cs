using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Hunter.Services.Dto
{
    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime Added { get; set; }
        public string Path { get; set; }
    }
}
