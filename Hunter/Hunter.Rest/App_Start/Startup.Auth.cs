using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using Hunter.Rest.Providers;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web;
using Microsoft.Owin.Extensions;
using Owin.Security.Providers.Untappd;
using AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode;


namespace Hunter.Rest
{
    public partial class Startup
    {
        private void ConfigureOAuthToken(IAppBuilder app)
        {
            var secret = Encoding.UTF8.GetBytes(Config.TokenSecret);

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>()
            {
                { "email", ClaimTypes.Name },
                { "id", ClaimTypes.NameIdentifier },
                { "role", ClaimTypes.Role }
            };
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenHandler = new CustomJwtSecurityTokenHandler(),
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningToken = new BinarySecretSecurityToken(secret),
                    },
                    Provider = new CookiesJwtOAuthProvider(),
                });
            //app.UseStageMarker(PipelineStage.Authenticate);
            //app.UseClaimsTransformationComponent();
            //app.UseStageMarker(PipelineStage.PostAuthorize);
        }

    }

    public static class AppBuilderExtensions
    {
        public static void UseClaimsTransformationComponent(this IAppBuilder appBuilder)
        {
            appBuilder.Use<ClaimsTransformationComponent>();
        }

        
    }

    class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        protected override JwtSecurityToken ValidateSignature(string token,
            TokenValidationParameters validationParameters)
        {
            var jwt = base.ValidateSignature(token, validationParameters);
            if (String.IsNullOrEmpty(jwt.Issuer))
            {
                jwt.Payload.AddClaim(new Claim("iss", "default")); // this is a hack
            }
            return jwt;
        }
    }

    public class ClaimsTransformationComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _nextComponent;

        public ClaimsTransformationComponent(Func<IDictionary<string, object>, Task> appFunc)
        {
            if (appFunc == null) throw new ArgumentNullException("AppFunc of next component");
            _nextComponent = appFunc;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                ClaimsIdentity claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
                Debug.WriteLine("User authenticated in OWIN middleware: {0}", claimsIdentity.IsAuthenticated);
                IEnumerable<Claim> claimsCollection = claimsPrincipal.Claims;
                foreach (Claim claim in claimsCollection)
                {
                    Debug.WriteLine("Claim type in OWIN: {0}, claim value type: {1}, claim value: {2}", claim.Type, claim.ValueType, claim.Value);
                }
                IEnumerable<Claim> finalClaims = await Transform(claimsCollection);
                ClaimsPrincipal extendedPrincipal = new ClaimsPrincipal(new ClaimsIdentity(finalClaims, "CustomAuthType"));
                Thread.CurrentPrincipal = extendedPrincipal;
                HttpContext.Current.User = extendedPrincipal;
                environment["server.User"] = extendedPrincipal;
            }
            await _nextComponent(environment);

            var newContext = HttpContext.Current.User;
        }

        private async Task<IEnumerable<Claim>> Transform(IEnumerable<Claim> initialClaims)
        {
            ClaimsTransformationService service = new ClaimsTransformationService();
            IEnumerable<Claim> finalClaims = await service.TransformInititalClaims(initialClaims);
            return finalClaims;
        }
    }

    public class ClaimsTransformationService
    {
        public Task<IEnumerable<Claim>> TransformInititalClaims(IEnumerable<Claim> initialClaims)
        {
            var result = new List<Claim>(initialClaims);
            result.Add(new Claim("transformed", "true"));
            return Task.FromResult<IEnumerable<Claim>>(result);
        }
    }

    public class CookiesJwtOAuthProvider : OAuthBearerAuthenticationProvider
    {
        //private  IUserProfileService _userProfileService;

        //public  CookiesJwtOAuthProvider()
        //{
        //    _userProfileService = NinjectContainer.Resolve<IUserProfileService>();
        //}



        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var token = context.Request.Cookies["x-access-token"];
            //token =
            //    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6IjU1ZGMxMzM5MTg0NmM2OGExYWQ1NmRhYSIsImVtYWlsIjoiYWRtaW5AYWRtaW4iLCJyb2xlIjoiQURNSU4iLCJpYXQiOjE0NDg0NjQzMjl9.w6mWO4M5lbI6i8qvlKn3t32z0uDfJxOzgRop1cfgV5s";
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            } else
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("referer", Config.OAuthReferer));
                HttpContext.Current.Response.Redirect(Config.ExternalAuthPath);
            }

            return Task.FromResult<object>(null);
        }

        //public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        //{
        //    IUserProfileService _userProfileService = NinjectContainer.Resolve<IUserProfileService>();

        //    if (!_userProfileService.UserExist(context.Ticket.Identity.Name))
        //    {
        //        _userProfileService.Save(new EditUserProfileVm()
        //        {
        //            Login = context.Ticket.Identity.Name,
        //            Position = context.Ticket.Identity.Name
        //        });
        //    }
        //    context.Validated();
        //    return Task.FromResult<object>(null);
        //}
    }
}
