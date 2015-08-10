using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.Services;

namespace Hunter.Rest.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("{roleName}")]
        public HttpResponseMessage GetByRoles(string roleName)
        {
            try 
            {
                var users = _userService.GetUsersByRole(roleName);
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            
        }
    }
}
