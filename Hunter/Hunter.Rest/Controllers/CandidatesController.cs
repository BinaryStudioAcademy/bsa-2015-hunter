using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;
using Hunter.Services.Interfaces;
using Hunter.Services;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/candidates")]
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
        [ResponseType(typeof(IEnumerable<CandidateDto>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = _candidateService.GetAllInfo().OrderByDescending(c => c.AddDate).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            
        }

        [HttpGet]
        [Route("odata")]
        public IHttpActionResult GetOdata(ODataQueryOptions<CandidateDto> options)
        {
            IQueryable results = options.ApplyTo(_candidateService.GetAllInfo().OrderByDescending(c => c.AddDate).AsQueryable());
            var result = new PageResult<CandidateDto>(
                results as IEnumerable<CandidateDto>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount);
            return Ok(result);
        }



        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(CandidateDto))]
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

        [HttpGet]
        [Route("longlist/{id:int}")]
        [ActionName("Longlist")]
        [ResponseType(typeof (CandidateLongListDto))]
        public HttpResponseMessage GetCandidateLongList(int id)
        {
            try
            {
                var candidates = _candidateService.GetLongList(id);
                if (candidates == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Vacatcy has no candidates!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, candidates);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("candidatelonglist/{id:int}")]
        [ActionName("Candidatelonglist")]
        [ResponseType(typeof(CandidateLongListDetailsDto))]
        public HttpResponseMessage GetCandidateLongListDetails(int id)
        {
            try
            {
                var candidate = _candidateService.GetLongListDetails(id);
                if (candidate == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Vacatcy has no candidates!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, candidate);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(CandidateDto candidate)
        {
            try
            {
                _candidateService.Add(candidate);
                return Request.CreateResponse(HttpStatusCode.Created, _candidateService.Get(i=>i.Email.Equals(candidate.Email)).ToCandidateDto());

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, CandidateDto candidate)
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

                return Request.CreateResponse(HttpStatusCode.OK, candidate);
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