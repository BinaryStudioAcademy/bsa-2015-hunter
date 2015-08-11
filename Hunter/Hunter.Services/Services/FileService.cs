using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class FileService : IFileService
    {
        private ICandidateService _candidateService;

        public FileService(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Add(FileDto file)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/"), file.Directory);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (file.Id == 0)
                file.Id = GetCandidateId(file.Email);


            string foolPath = path +
                              String.Format("{0}##{1}##{2}{3}", file.Id, DateTime.Now.Date.ToShortDateString(), file.Email, Path.GetExtension(file.File.FileName));

            file.File.SaveAs(foolPath);
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


        private int GetCandidateId(string email)
        {
            return _candidateService.Get(i=>i.Email==email).Id;
        }
    }
}
