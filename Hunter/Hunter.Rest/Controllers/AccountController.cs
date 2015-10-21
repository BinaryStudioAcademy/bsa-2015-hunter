using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Hunter.Rest.Models;
using Hunter.Services;
using JWT;

namespace Hunter.Rest.Controllers
{
    public class AccountController : ApiController
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Login(LocalLogin login)
        {
            var user = _userService.IsValidUser(login.UserName, login.Password);

            if (user != null)
            {
                var sekretKey = WebConfigurationManager.AppSettings["secret"];
                var payload = new Dictionary<string, object>()
                {
                    {"id", user.Id},
                    {"email", user.Login},
                    {"role",user.UserRole.Name }
                };

                var jwtToken = JsonWebToken.Encode(payload, sekretKey, JwtHashAlgorithm.HS256);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("x-access-token", jwtToken));

                var referer = HttpContext.Current.Request.Cookies.Get("referer");

                return Ok(new {referer = referer != null ?referer.Value : ""});
            }
            else
            {
                return BadRequest("Wrong login or password!");
            }
        }

    }
}
