using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using Hunter.Services.Dto.User;
using Hunter.Services.Interfaces;

namespace Hunter.Rest.Providers
{
    public class ExternalAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IUserProfileService _userProfileService = NinjectContainer.Resolve<IUserProfileService>();
            if (!_userProfileService.UserExist(actionContext.RequestContext.Principal.Identity.Name))
                {
                _userProfileService.Save(new EditUserProfileVm()
                {
                    Login = actionContext.RequestContext.Principal.Identity.Name,
                    Position = actionContext.RequestContext.Principal.Identity.Name
                });
            }
            base.OnAuthorization(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            actionContext.Response.Headers.Location = new Uri(WebConfigurationManager.AppSettings["authUrl"]);
        }
    }
}