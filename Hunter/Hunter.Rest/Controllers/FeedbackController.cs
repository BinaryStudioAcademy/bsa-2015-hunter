using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services.Dto;
using System.Web.Http.Description;
using Hunter.Services;

namespace Hunter.Rest.Controllers
{
    [Authorize]
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedback
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("hr/{vid:int}/{cid:int}")]
        //[ActionName("HrInterview")]
        [ResponseType(typeof(IEnumerable<FeedbackHrInterviewDto>))]
        public HttpResponseMessage GetHrInterview(int vid, int cid)
        {
            try
            {
                var interviews = _feedbackService.GetAllHrInterviews(vid, cid);
                return interviews == null ? Request.CreateResponse(HttpStatusCode.BadRequest, "Vacancy has no candidates!") 
                    : Request.CreateResponse(HttpStatusCode.OK, interviews);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Feedback/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Feedback
        [HttpPost]
        [Route("hr/save")]
        public HttpResponseMessage Post([FromBody]FeedbackHrInterviewDto hrInterviewDto)
        {
            try
            {
                _feedbackService.SaveFeedback(hrInterviewDto, User.Identity.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "Feedback was edited/saved!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            

        }

        // PUT: api/Feedback/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Feedback/5
        public void Delete(int id)
        {
        }
    }
}
