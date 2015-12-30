using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Hunter.Services.Services.Interfaces;
using Microsoft.Owin.Security.Provider;
using WebGrease.Css.Extensions;

namespace Hunter.Rest.Providers
{
    public class CheckRole: AuthorizeAttribute
    {
        public string[] Role { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IRoleMappingServiceCache userProfileService = NinjectContainer.Resolve<IRoleMappingServiceCache>();

            ClaimsIdentity claimsIdentity;

            claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var authRole = claimsIdentity.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value;

            var hunterRole = userProfileService.TransformRole(authRole);

            foreach (var role in Role)
            {
                if (role == hunterRole)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "havent permissions");
        }
    }
}