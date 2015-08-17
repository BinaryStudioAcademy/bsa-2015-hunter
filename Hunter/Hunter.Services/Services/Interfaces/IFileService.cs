﻿
using System.Collections.Generic;
using System.Net.Http;
using Hunter.Services.Dto;

namespace Hunter.Services.Interfaces
{
    public interface IFileService
    {
        void Add(FileDto file);
        void Update(FileDto file);
        FileDto Get(string fileName);
        IEnumerable<FileDto> GetAll();
        void Delete(string fileName);
        ByteArrayContent GetPhoto(int id);
    }
}
