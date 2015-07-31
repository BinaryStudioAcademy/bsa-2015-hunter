using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Hunter.Rest.Models;
using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;
using Hunter.Services;
using Microsoft.Owin.Security.DataProtection;

namespace Hunter.Rest
{
    public class HunterUserStore : IUserStore<User, int>,
            IUserPasswordStore<User, int>,
            IUserRoleStore<User, int>

    {
        private readonly IUserService _userServices;

        public HunterUserStore(IUserService userServices)
        {
            _userServices = userServices;
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            _userServices.AddToRole(user, roleName);
            return Task.FromResult<object>(null);
        }

        public Task CreateAsync(User user)
        {
            _userServices.CreateUser(user);
            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(User user)
        {
            _userServices.DeleteUser(user);
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            var user = _userServices.GetUserById(userId);
            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.FromResult(_userServices.GetUserByName(userName));
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult<IList<string>>(new List<string> { user.UserRole.Name });
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!user.PasswordHash.IsEmpty());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(user.UserRole.Name.ToLower() == roleName.ToLower());
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<Object>(null);
        }

        public Task UpdateAsync(User user)
        {
            _userServices.UpdateUser(user);
            return Task.FromResult<object>(null);
        }
    }



    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));


        }


    }
}
