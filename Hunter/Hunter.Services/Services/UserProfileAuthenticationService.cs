using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Interface;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class UserProfileAuthenticationService : IUserProfileAuthenticationService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRoleService _roleService;

        public UserProfileAuthenticationService(IUserProfileRepository userProfileRepository, IUserRoleService userRoleService)
        {
            _userProfileRepository = userProfileRepository;
            _roleService = userRoleService;
        }      

        public int GetUserIdByUserLogin(string userLogin)
        {
            return _userProfileRepository.Get(x => x.UserLogin == userLogin).Id;
        }
        public IEnumerable<string> GetAuthUserIdByRole(string roleName, string userName)
        {
            try
            {
                var roles = _roleService.GetRoleByName(roleName).RoleMapping;
                var users = _userProfileRepository.Query().Where(u => u.UserLogin != userName).ToList();

                var us = from role in roles
                         join user in users on role.Position.ToLower() equals user.Position.ToLower()
                         select user.AuthUserId;

                return us.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
