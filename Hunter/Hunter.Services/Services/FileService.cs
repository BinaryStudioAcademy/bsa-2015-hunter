using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;
using Hunter.DataAccess.Interface.Repositories;

namespace Hunter.Services
{
    public class FileService : IFileService
    {
        private IFileRepository _fileRepository;
        private readonly ILogger _logger;

        private string _localStorage = HttpContext.Current.Server.MapPath("~/App_Data/Hunter/Files/");

        public FileService(IFileRepository fileRepository, ILogger logger)
        {
            _fileRepository = fileRepository;
            _logger = logger;
        }

        public int Add(FileDto file)
        {
            file.Added = DateTime.Now;



            var formatedFileName = FormatFileName(file);
            switch (file.FileType)
            {
                case FileType.Resume:
                    file.Path = "Resume\\";
                    break;
                case FileType.Test:
                    file.Path = string.Format("Test\\{0}\\", file.VacancyId);
                    break;
                case FileType.Other:
                    file.Path = "Other\\";
                    break;
            }
            _localStorage = Path.Combine(_localStorage, file.Path);

            if (!Directory.Exists(_localStorage))
                Directory.CreateDirectory(_localStorage);

            string fileName = Path.Combine(_localStorage, formatedFileName);
            try
            {
                SaveFile(file.File, fileName);
                
                var newFile = file.ToFile();
                _fileRepository.UpdateAndCommit(newFile);
                return newFile.Id;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return -1;
        }

        private string FormatFileName(FileDto file)
        {
            return string.Format("{0}##{1}##{2}", file.CandidateId, file.Added.ToShortDateString(), file.FileName);
        }

        private void SaveFile(Stream file, string fileName)
        {
            using (var fileStream = File.Open(fileName, FileMode.Create))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        private Stream LoadFile(string fileName)
        {
            if (!File.Exists(fileName)) return null;

            Stream stream = null;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                stream = new MemoryStream();
                fileStream.CopyTo(stream);
            }
            return stream;
        }

        public void Update(FileDto file)
        {
            file.Added = DateTime.Now;
            var entity = _fileRepository.Get(file.Id);
            if (entity == null) return;
            var fileName = GetFullPath(entity.ToFileDto());
            if (File.Exists(fileName))
                File.Delete(fileName);
            file.ToFile(entity);
            _fileRepository.UpdateAndCommit(entity);
            SaveFile(file.File, GetFullPath(file));
        }

        public FileDto Get(int id)
        {
            var entity = _fileRepository.Get(id);
            if (entity != null)
            {
                var dto = entity.ToFileDto();
                //dto.File = LoadFile(GetFullPath(dto));
                return dto;
            }
            return null;
        }

        public IEnumerable<FileDto> Get()
        {
            var files = _fileRepository.All().Select(f => f.ToFileDto());
            //foreach (var file in files)
            //    file.File = LoadFile(GetFullPath(file));
            return files;
        }

        private string GetFullPath(FileDto file)
        {
            var formatedFileName = FormatFileName(file);
            return _localStorage + file.Path + formatedFileName;
        }

        public void Delete(int id)
        {
            var entity = _fileRepository.Get(id);
            if (entity != null)
            {
                var fileName = GetFullPath(entity.ToFileDto());
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    _fileRepository.DeleteAndCommit(entity);
                }
            }
        }

        public FileDto DownloadFile(int id)
        {
            var entity = _fileRepository.Get(id);
            if (entity != null)
            {
                var dto = entity.ToFileDto();
                dto.File = LoadFile(GetFullPath(dto));
                return dto;
            }
            return null;
        }
    }
}