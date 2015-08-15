using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class FileExtension
    {
        public static FileDto ToFileDto(this File file)
        {
            return new FileDto
            {
                Id = file.Id,
                Added = file.Added,
                FileName = file.FileName,
                Path = file.Path
            };
        }

        public static void ToFile(this FileDto fileDto, File file)
        {
            file.Id = fileDto.Id;
            file.Path = fileDto.Path;
            file.Added = fileDto.Added;
            file.FileName = fileDto.FileName;
        }
    }
}
