using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Hunter.Rest.Providers
{
    public class CheckRole: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //base.HandleUnauthorizedRequest(actionContext);
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "havent permissions");

        }
    }
}