using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;
using Hunter.DataAccess.Interface.Repositories;
using File = System.IO.File;

namespace Hunter.Services
{
    public class FileService : IFileService
    {
        private IFileRepository _fileRepository;
        private readonly ILogger _logger;
        private ICandidateService _candidateService;
        private IResumeRepository _resumeRepository;

        private string _localStorage = HttpContext.Current.Server.MapPath("~/App_Data/Hunter/Files/");

        public FileService(IFileRepository fileRepository, ILogger logger, ICandidateService candidateService,
            IResumeRepository resumeRepository)
        {
            _fileRepository = fileRepository;
            _logger = logger;
            _candidateService = candidateService;
            _resumeRepository = resumeRepository;
        }

        public int Add(FileDto file)
        {
            file.Added = DateTime.Now;

            //_logger.Log(string.Format("{0}, {1}", file.CandidateId, file.VacancyId));

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
                newFile.FileName = formatedFileName;
                _fileRepository.UpdateAndCommit(newFile);

                if (file.FileType == FileType.Resume)
                {
                    var candidate = _candidateService.Get(file.CandidateId);
                    if (candidate.ResumeId != null)
                    {
                        var resume = _resumeRepository.Get((int)candidate.ResumeId);
                        resume.FileId = newFile.Id;
                        _resumeRepository.UpdateAndCommit(resume);
                    }
                    else
                    {
                        var resume = new Resume() { FileId = newFile.Id };
                        resumeRepository.UpdateAndCommit(resume);
                        candidate.ResumeId = resume.Id;
                        _candidateService.Update(candidate.ToCandidateDto());
                    }
                                                               
                }    

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
            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                stream = new MemoryStream();
                fileStream.CopyTo(stream);
            }
            return stream;
        }

        public ByteArrayContent GetPhoto(int id)
        {
            var array = _candidateService.Get(id).Photo;
            if (array != null)
            {
                return new ByteArrayContent(array);
            }
            else
            {
                array = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/App_Data/no_photo.png"));
                return new ByteArrayContent(array);
            }
        }

        public FileDto GetResumeFileDto(int resumeId)
        {
            var resume = _resumeRepository.Get(resumeId);
            var fileDto = _fileRepository.Get((int)resume.FileId).ToFileDto();
            return fileDto;
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
            var files = _fileRepository.Query().ToList().Select(f => f.ToFileDto());
            //foreach (var file in files)
            //    file.File = LoadFile(GetFullPath(file));
            return files;
        }

        private string GetFullPath(FileDto file)
        {
            return _localStorage + file.Path + file.FileName;
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
                dto.File.Position = 0;
                return dto;
            }
            return null;
        }

        public void UploadPhotoFromUrl(string source, int candidateId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var wr = WebRequest.Create(source);
                var response = wr.GetResponse();
                Stream stream = response.GetResponseStream();
                stream.CopyTo(ms);
                var candidate = _candidateService.Get(candidateId);
                candidate.Photo = ms.ToArray();
                _candidateService.Update(candidate.ToCandidateDto());
            }        
        }
    }
}
