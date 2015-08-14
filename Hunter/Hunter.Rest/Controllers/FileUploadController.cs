using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Hunter.DataAccess.Entities;
using Hunter.Services;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/fileupload")]
    public class FileUploadController : ApiController
    {
        private IFileService _fileService;
        private ICandidateService _candidateService;

        public FileUploadController(IFileService fileService, ICandidateService candidateService)
        {
            _fileService = fileService;
            _candidateService = candidateService;
        }

        [HttpGet]
        [Route("pictures/{id:int}")]
        public HttpResponseMessage GetPicture(int id)
        {
            var array = _candidateService.Get(id).Photo;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            if (array != null)
            {
                result.Content = new ByteArrayContent(array);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }
            else
            {
                result.StatusCode = HttpStatusCode.NotFound;
            }
            return result;
        }


        [HttpPost]
        [Route("pictures/{id:int}")]
        public async Task<HttpResponseMessage> PostPicture(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                byte[] ms = await provider.Contents[0].ReadAsByteArrayAsync();
                var candidate = _candidateService.Get(id);

                candidate.Photo = ms;
                _candidateService.Update(candidate.ToCandidateDto());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete]
        [Route("pictures/{id:int}")]
        public HttpResponseMessage DeletePicture(int id)
        {
            var candidate = _candidateService.Get(id);
            if (candidate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                candidate.Photo = null;
                _candidateService.Update(candidate.ToCandidateDto());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public void Post(FileDto data)
        {
            _fileService.Add(data);
        }
    }
}