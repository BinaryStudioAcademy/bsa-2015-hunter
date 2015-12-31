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
    [CheckRole(Role = new []{"Admin"})]
    [RoutePrefix("api/roleMapping")]
    public class RoleMappingController : ApiController
    {
        private readonly IRoleMappingService _roleMappingService;

        public RoleMappingController(IRoleMappingService roleMappingService)
        {
            _roleMappingService = roleMappingService;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<RoleMappingDto>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = _roleMappingService.GetAllInfo();
                var a = data.OrderByDescending(c => c.Position).ToUseRoleMappingDtos().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put(RoleMappingDto roleMappingDto)
        {
            try
            {
                _roleMappingService.Update(roleMappingDto.ToRoleMapping());
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }


}
