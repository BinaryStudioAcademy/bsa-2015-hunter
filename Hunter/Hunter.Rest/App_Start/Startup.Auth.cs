using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Web.Configuration;
using Hunter.Rest.Providers;
using Microsoft.Owin.Security.Jwt;
using Owin;
using AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode;

namespace Hunter.Rest
{
    public partial class Startup
    {
        private void ConfigureOAuthToken(IAppBuilder app)
        {
            var secret = Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["secret"]);

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
                    Provider = new CookiesJwtOAuthProvider(),
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
