﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.DataAccess.Db;
using Hunter.Services;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/activities")]
    [Authorize]
    public class ActivityController : ApiController
    {
        private readonly IActivityService _activityService;
        private readonly IUserProfileService _userProfileService;

        public ActivityController(IActivityService activityService, IUserProfileService userProfileService)
        {
            _activityService = activityService;
            _userProfileService = userProfileService;
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
                    throw new Exception("Access denied");
                }

                var profile = _userProfileService.GetUserProfile(login);
                return Ok(_activityService.GetAmountOfActualActivities(profile.LastViewedActivityId));
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

                var userProfile = _userProfileService.GetUserProfile(login);
                userProfile.LastViewedActivityId = lastActivityId;

                _userProfileService.UpdateUserProfile(userProfile);

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
