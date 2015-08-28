﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Hunter.Services;
using Hunter.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [Authorize]
    [RoutePrefix("api/notifications")]
    public class ScheduledNotificationController : ApiController
    {
        private readonly IScheduledNotificationService _scheduledNotificationService;

        public ScheduledNotificationController(IScheduledNotificationService scheduledNotificationService)
        {
            _scheduledNotificationService = scheduledNotificationService;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<ScheduledNotificationDto>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var login = RequestContext.Principal.Identity.Name;
                var notifications = _scheduledNotificationService.Get(login);
                return Request.CreateResponse(HttpStatusCode.OK, notifications);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("active")]
        [ResponseType(typeof(IEnumerable<ScheduledNotificationDto>))]
        public HttpResponseMessage GetActive()
        {
            try
            {
                var login = RequestContext.Principal.Identity.Name;
                var notifications = _scheduledNotificationService.GetActive(login);
                return Request.CreateResponse(HttpStatusCode.OK, notifications);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(ScheduledNotificationDto))]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var notification = _scheduledNotificationService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, notification);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]ScheduledNotificationDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = RequestContext.Principal.Identity.Name;
                    value.UserLogin = login;
                    _scheduledNotificationService.Add(value);
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
        public HttpResponseMessage Put(int id, [FromBody]ScheduledNotificationDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _scheduledNotificationService.Update(value);
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
        [Route("{id:int}/shown")]
        public HttpResponseMessage NotificationShowed(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _scheduledNotificationService.NotificationShown(id);
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
                _scheduledNotificationService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("notify")]
        public HttpResponseMessage Notify()
        {
            try
            {
                _scheduledNotificationService.Notify();
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}