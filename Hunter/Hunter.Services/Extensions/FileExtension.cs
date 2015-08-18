using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services
{
    public static class FileExtension
    {
        public static FileDto ToFileDto(this File file)
        {
            return new FileDto
            {
                Id = file.Id,
                Added = file.Added,
                FileName = cutNamePrefix(file.FileName),
                Path = file.Path
            };
        }

        public static File ToFile(this FileDto fileDto)
        {
            var file = new File
            {
                //file.Id = fileDto.Id;
                Path = fileDto.Path,
                Added = fileDto.Added,
                FileName = fileDto.FileName
            };
            return file;
        }
        public static void ToFile(this FileDto fileDto, File file)
        {
            //file.Id = fileDto.Id;
            file.Path = fileDto.Path;
            file.Added = fileDto.Added;
            file.FileName = fileDto.FileName;
        }

        private static string cutNamePrefix(string name)
        {
            var parts = name.Split(new string[] { "##" }, StringSplitOptions.None);

            return parts.Length >= 3 ? parts[2] : parts[0];
        }
    }
}
