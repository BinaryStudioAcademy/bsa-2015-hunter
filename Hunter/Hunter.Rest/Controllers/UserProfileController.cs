﻿using System;
using System.Globalization;
using System.Net;
using System.Web.Http.Results;
using Hunter.Common.Concrete;
using Hunter.Services;
using System.Collections.Generic;
using System.Web.Http;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;
using Hunter.Services.Interfaces;

namespace Hunter.Rest
{
    public class UserProfileController : ApiController
    {
        private readonly IUserProfileService _profileService;

        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: api/userprofile?page=1
        [System.Web.Mvc.HttpGet]
        public IEnumerable<UserProfileRowVm> GetPage([FromUri(Name = "page")]int page = 1)
        {
            return _profileService.LoadPage(page);
        }

        // GET: api/Pool/5
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            return FromApiResult(() => _profileService.GetById(id));
        }

        // POST: api/Pool - add new profile
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post([FromBody] EditUserProfileVm profileVm)
        {
            return FromApiResult(() => _profileService.Save(profileVm));
        }

        // DELETE: api/Pool/5
        [System.Web.Mvc.HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return FromApiResult(() => _profileService.Delete(id));
        }

        private IHttpActionResult FromApiResult(Func<ApiResult> apiResultFunc)
        {
            if (apiResultFunc == null)
            {
                Logger.Instance.Log("function not passed to FromApiResult");
                return InternalServerError();
            }

            ApiResult result = null;

            try
            {
                result = apiResultFunc();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            var resourceResult = result as ResourceApiResult;

            if (resourceResult != null && !resourceResult.IsError)
            {
                if (resourceResult.Code == HttpStatusCode.Created)
                {
                    return Created(Request.RequestUri + resourceResult.Id.ToString(CultureInfo.InvariantCulture),
                                   resourceResult.Resource);
                }
                if (resourceResult.Code == HttpStatusCode.OK)
                {
                    return Ok(resourceResult.Resource);
                }

                return StatusCode(resourceResult.Code);
            }

            if (result.IsError)
            {
                return Content(result.Code, result.Message);
            }

            return StatusCode(result.Code);
        }
    }
}