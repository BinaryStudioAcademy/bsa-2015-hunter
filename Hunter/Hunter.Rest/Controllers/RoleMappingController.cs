using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Hunter.DataAccess.Entities.Entites;
using Hunter.Rest.Providers;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Rest.Controllers
{
    [ExternalAuthorize]
    [RoutePrefix("api/roleMapping")]
    public class RoleMappingController : ApiController
    {
        private readonly IUserRoleMappingService _userRoleMappingService;

        public RoleMappingController(IUserRoleMappingService userRoleMappingService)
        {
            _userRoleMappingService = userRoleMappingService;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<RoleMappingDto>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = _userRoleMappingService.GetAllInfo().OrderByDescending(c => c.Position).ToUseRoleMappingDtos().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

    }
}
