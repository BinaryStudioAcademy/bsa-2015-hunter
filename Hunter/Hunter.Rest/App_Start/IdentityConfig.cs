using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Hunter.Rest.Models;
using Hunter.DataAccess.Db;
using System;
using System.Collections.Generic;
using Hunter.Services;
using Microsoft.Owin.Security.DataProtection;

namespace Hunter.Rest
{
    public class HunterUserStore : IUserStore<User, int>,
            IUserPasswordStore<User, int>,
            IUserRoleStore<User, int>

    {
        private readonly UserServices _userServices;

        public HunterUserStore(UserServices userServices)
        {
            _userServices = userServices;
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
           _userServices.AddToRole(user,roleName);
            return new Task<object>(null);
        }

        public Task CreateAsync(User user)
        {
            _userServices.CreateUser(user);
            return new Task<object>(null);
        }

        public Task DeleteAsync(User user)
        {
            _userServices.DeleteUser(user);
            return new Task<object>(null);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            var user = _userServices.GetUserById(userId);
            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
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
