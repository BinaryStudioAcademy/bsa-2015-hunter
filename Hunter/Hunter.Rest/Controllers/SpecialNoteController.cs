using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services.Dto;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/specialnote")]
    public class SpecialNoteController : ApiController
    {
        private readonly ISpecialNoteService _specialNoteService;

        public SpecialNoteController(ISpecialNoteService specialNoteService)
        {
            _specialNoteService = specialNoteService;
        }


        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetSpecialNote()
        {
            try
            {
                var specialNotes = _specialNoteService.GetAllSpecialNotes();
                return Request.CreateResponse(HttpStatusCode.OK, specialNotes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("candidate/{cid}")]
        public HttpResponseMessage GetSpecialNoteForCandidate(int cid)
        {
            try
            {
                var specialNotes = _specialNoteService.GetSpecialNotesForCandidate(cid);
                return Request.CreateResponse(HttpStatusCode.OK, specialNotes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{vid}/{cid}")]
        public HttpResponseMessage GetSpecialNoteForCard(int vid,int cid)
        {
            try
            {
                var specialNotes = _specialNoteService.GetSpecialNotesForCard(vid,cid);
                return Request.CreateResponse(HttpStatusCode.OK, specialNotes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("user/{login}")]
        public HttpResponseMessage GetSpecialNoteForUser(string login)
        {
            try
            {
                var specialNotes = _specialNoteService.GetSpecialNotesForUser(login);
                return Request.CreateResponse(HttpStatusCode.OK, specialNotes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetSpecialNoteById(int id)
        {
            try
            {
                var specialNotes = _specialNoteService.GetSpecialNoteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, specialNotes);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostSpecialNote(SpecialNoteDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _specialNoteService.AddSpecialNote(model);
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
        [Route("")]
        public HttpResponseMessage UpdateSpecialNote(SpecialNoteDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _specialNoteService.UpdateSpecialNote(model);
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
        [Route("{id}")]
        public HttpResponseMessage DeleteSpecialNote(int id)
        {
            try
            {
                _specialNoteService.DeleteSpecialNoteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
