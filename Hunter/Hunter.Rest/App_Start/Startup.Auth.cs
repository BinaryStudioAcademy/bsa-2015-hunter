using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Hunter.Rest.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Ninject.Web.Common.OwinHost;
using Owin;
using Owin.Security.Providers.LinkedIn;
using System.Threading.Tasks;
using System.Xml;
using Hunter.Services.Interfaces;

namespace Hunter.Rest
{
    public partial class Startup
    {
        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        //internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        //  public void ConfigureAuth(IAppBuilder app)
        //  {
        //  DataProtectionProvider = app.GetDataProtectionProvider();
        // Configure the db context and user manager to use a single instance per request
        //app.CreatePerOwinContext(ApplicationDbContext.Create);
        // app.CreatePerOwinContext(CreateKernel);
        // app.UseNinjectMiddleware(CreateKernel);
        // app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

        // Enable the application to use a cookie to store information for the signed in user
        // and to use a cookie to temporarily store information about a user logging in with a third party login provider
        // app.UseCookieAuthentication(new CookieAuthenticationOptions());
        //  app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


        // Configure the application for OAuth based flow
        //   PublicClientId = "self";
        //   OAuthOptions = new OAuthAuthorizationServerOptions
        //{
        //    TokenEndpointPath = new PathString("/Token"),
        //    Provider = new ApplicationOAuthProvider(PublicClientId),
        //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
        //    AccessTokenFormat = new HunterJwtFormat("http://localhost:53147/"),
        //    // In production mode set AllowInsecureHttp = false
        //    AllowInsecureHttp = true
        //};

        // Enable the application to use bearer tokens to authenticate users
        //app.UseOAuthBearerTokens(OAuthOptions);
        // app.UseOAuthAuthorizationServer(OAuthOptions);
        // Uncomment the following lines to enable logging in with third party login providers
        //app.UseLinkedInAuthentication(
        //    "<YOUR API KEY>", 
        //    "<YOUR SECRET KEY>"
        //    );
        //  }

        private void ConfigureOAuthToken(IAppBuilder app)
        {
            var secret = Encoding.UTF8.GetBytes("superpupersecret");

            CustomJwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>()
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
                    Provider = new CookiesJwtOAuthProvider()

                });
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
}
