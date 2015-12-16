using System;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Hunter.Rest.Models;
using Hunter.Rest.Providers;
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
            if (Config.UseExternalAuth)
            {
                return Redirect(Config.ExternalPath);
            }

            var user = _userService.IsValidUser(login.UserName, login.Password);

            if (user != null)
            {
                var payload = new Dictionary<string, object>()
                {
                    {"id", user.Id},
                    {"email", user.Login},
                    {"role",user.UserRole.Name }
                };

                var jwtToken = JsonWebToken.Encode(payload, Config.TokenSecret, JwtHashAlgorithm.HS256);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("x-access-token", jwtToken));

                var referer = HttpContext.Current.Request.Cookies.Get("referer");

                return Ok(new {referer = referer != null ?referer.Value : ""});
            }
            else
            {
                return BadRequest("Wrong login or password!");
            }
        }

        [HttpPost]
        [Route("api/something")]
        public HttpResponseMessage LogOut()
        {
            if (HttpContext.Current.Request.Cookies["x-access-token"] != null)
            {
                HttpCookie myCookie = new HttpCookie("x-access-token");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            //HttpContext.Current.Response.Headers.Remove("x-access-token");
            ////HttpContext.Current.Response.Cookies.Add(new HttpCookie("referer", Config.OAuthReferer));
            ////return Ok();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
