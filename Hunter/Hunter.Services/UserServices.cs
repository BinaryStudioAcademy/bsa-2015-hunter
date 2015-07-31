using Hunter.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;

namespace Hunter.Services
{
    public class UserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
        }

        public User GetUserByName(string name)
        {
            return _userRepository.Get(u => u.UserName.ToLower() == name.ToLower());
        }

        public User GetUserById(int id)
        {
            return _userRepository.Get(id);
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
            _unitOfWork.SaveChanges();
        }

        public void AddToRole(User user, string roleName)
        {
            var role = _userRoleRepository.Get(r => r.Name.ToLower() == roleName.ToLower());
            if (role != null)
            {
                user.RoleId = role.Id;
                _userRepository.Update(user);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
            _unitOfWork.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
       

    }
}
