﻿using Hunter.Services;
using Hunter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<FileDto>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var files = _fileService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(FileDto))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var file = _fileService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, file);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]FileDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _fileService.Add(value);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody]FileDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _fileService.Update(value);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _fileService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(FileDto))]
        public HttpResponseMessage DownloadFile(int id)
        {
            try
            {
                var file = _fileService.DownloadFile(id);

                return Request.CreateResponse(HttpStatusCode.OK, file);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}