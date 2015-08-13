using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services;
using Hunter.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/activities")]
    [Authorize]
    public class ActivityController : ApiController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

       
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetActivities()
        {        
            try
            {
                var activities = _activityService.GetAllActivities();
                return Request.CreateResponse(HttpStatusCode.OK, activities);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("amount")]
        public IHttpActionResult GetActivitiesAmount()
        {
            try
            {
                var login = RequestContext.Principal.Identity.Name;

                if (login == null)
                {
                    // todo how it could be if we already authorized
                    throw new Exception("Access denied");
                }

                return Ok(_activityService.GetUnreadActivitiesForUser(login));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("save/lastid")]
        public IHttpActionResult SaveLastViewdActivityId([FromBody] int lastActivityId)
        {
            try
            {
                string login = RequestContext.Principal.Identity.Name;
                _activityService.UpdateLastSeenActivity(login, lastActivityId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetActivityById(int id)
        {
            try
            {
                var activity = _activityService.GetActivityById(id);
                return Request.CreateResponse(HttpStatusCode.OK, activity);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostActivity(ActivityDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _activityService.AddActivity(model);
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
        public HttpResponseMessage UpdateActivity(ActivityDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _activityService.UpdateActivity(model);
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
        public HttpResponseMessage DeleteActivity(int id)
        {
            try
            {
                _activityService.DeleteActivityById(id); 
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
