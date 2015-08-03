using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.Services;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/Candidates")]
    public class CandidatesController : ApiController
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public CandidatesController()
        {
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = _candidateService.GetAllInfo();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = _candidateService.GetInfo(id);
                
                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _candidateService.Add(candidate);
                return Request.CreateResponse(HttpStatusCode.Created, candidate);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, Candidate candidate)
        {
            if (ModelState.IsValid && id == candidate.Id)
            {
                try
                {
                    _candidateService.Update(candidate);
                }
                catch (Exception e)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            var candidate = _candidateService.Get(id);
            
            if (candidate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _candidateService.Delete(candidate);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}