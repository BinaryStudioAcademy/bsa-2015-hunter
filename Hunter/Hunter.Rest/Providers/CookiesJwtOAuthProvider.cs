using System.Threading.Tasks;
using Hunter.Services.Dto.User;
using Hunter.Services.Interfaces;
using Microsoft.Owin.Security.OAuth;
using Ninject;

namespace Hunter.Rest.Providers
{
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
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            IUserProfileService _userProfileService = NinjectContainer.Resolve<IUserProfileService>();

            if (!_userProfileService.UserExist(context.Ticket.Identity.Name))
            {
                _userProfileService.Save(new EditUserProfileVm()
                {
                    Login = context.Ticket.Identity.Name,
                    Position = context.Ticket.Identity.Name
                });
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}