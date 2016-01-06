using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Hunter.Services.Dto.User;
using Hunter.Services.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Hunter.Rest.Providers
{
    public class ExternalAuthorizeAttribute : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            Contract.Assert(actionContext != null);

            if (SkipAuthorization(actionContext)) return;

            IUserProfileService userProfileService = NinjectContainer.Resolve<IUserProfileService>();
            // TODO new recognise alias and position
            if (Config.UseExternalAuth)
            {
                //actionContext.RequestContext.Principal.Identity.Name != null &&
                if (!userProfileService.UserExist(actionContext.RequestContext.Principal.Identity.Name))
                {
                    var user =
                        await
                            userProfileService.CreateUserAlias(Config.ExternalAuthPath + "api/users/" +
                                                               actionContext.RequestContext.Principal.Identity.GetUserId
                                                                   ());
                    if (user != null)
                    {
                        userProfileService.Save(new EditUserProfileVm()
                        {
                            Login = user.Email ?? "Unknown",
                            Alias = user.Name ?? "Unknown",
                            Position = user.Role ?? "Unknown",
                            AuthUserId = user.AuthUserId ?? "Unknown"
                        });

                    }
                }
            }
            await base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            if (Config.UseExternalAuth)
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                actionContext.Response.Headers.Location = new Uri(Config.ExternalAuthPath);
            }
            else
            {
                // current local auth controller clones API of external server
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                actionContext.Response.Headers.Location = Config.GetLocalLoginUri();
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}