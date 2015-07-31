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
    [RoutePrefix("api/Candidate")]
    public class CandidateController : ApiController
    {
        private ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public CandidateController()
        {
        }

        [HttpGet]
        [Route("All")]
        public IEnumerable<Candidate> Get()
        {
            return _candidateService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Candidate Get(int id)
        {
            var candidate = _candidateService.Get(id);
            if (candidate == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return candidate;
        }

        [HttpPost]
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
                catch (Exception)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound);
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