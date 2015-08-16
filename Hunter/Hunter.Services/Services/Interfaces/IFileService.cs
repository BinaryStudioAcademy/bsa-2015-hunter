
using System.Collections.Generic;
using Hunter.Services.Dto;

namespace Hunter.Services.Interfaces
{
    public interface IFileService
    {
        void Add(FileDto file);
        void Update(FileDto file);
        FileDto Get(int id);
        IEnumerable<FileDto> Get();
        void Delete(int id);
        FileDto DownloadFile(int id);
    }
}
