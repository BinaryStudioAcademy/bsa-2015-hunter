using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Net.Http;
using Hunter.Common.Interfaces;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class FileService : IFileService
    {
        private ICandidateService _candidateService;
        private readonly ILogger _logger;

        public FileService(ICandidateService candidateService, ILogger loger)
        {
            _candidateService = candidateService;
            _logger = loger;
        }

        public void Add(FileDto fileContext)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/"), fileContext.Path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string foolPath = path +
                              String.Format("{0}##{1}##{2}", fileContext.Id, DateTime.Now.Date.ToShortDateString(), fileContext.FileName);

            try
            {
                //using (var fileStream = File.Open(foolPath, FileMode.Create))
                //{
                //    byte[] bytesInStream = new byte[fileContext.File.Length];
                //    fileContext.File.Read(bytesInStream, 0, (int)fileContext.File.Length);
                //    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                //}
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            
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

        public void Update(FileDto file)
        {
            throw new NotImplementedException();
        }

        public FileDto Get(string fileName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(string fileName)
        {
            throw new NotImplementedException();
        }

    }
}
