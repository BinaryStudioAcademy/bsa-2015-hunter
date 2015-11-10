using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Assert(actionContext != null);

            if (skipAuthorization(actionContext)) return;

            IUserProfileService _userProfileService = NinjectContainer.Resolve<IUserProfileService>();
            if (!_userProfileService.UserExist(actionContext.RequestContext.Principal.Identity.Name))
                {
                _userProfileService.Save(new EditUserProfileVm()
                {
                    Login = actionContext.RequestContext.Principal.Identity.Name,
                    Alias = "NEW",
                    Position = "Unconfirmed",
                });
            }
            base.OnAuthorization(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            if (Config.UseExternalAuth)
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                actionContext.Response.Headers.Location = new Uri(Config.ExternalPath);
            }
            else
            {
                // current local auth controller clones API of external server
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                actionContext.Response.Headers.Location = Config.GetLocalLoginUri();
            }
        }

        private static bool skipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}