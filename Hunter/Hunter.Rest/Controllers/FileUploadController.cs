using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;
using Newtonsoft.Json;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/fileupload")]
    public class FileUploadController : ApiController
    {
        private IFileService _fileService;

        public FileUploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post()
        {
//            HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["file"];
            var data = JsonConvert.DeserializeObject<FileDto>(HttpContext.Current.Request.Form["data"]) ;
            data.File = HttpContext.Current.Request.Files["file"];
            _fileService.Add(data);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}