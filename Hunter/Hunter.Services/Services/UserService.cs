using System.Collections.Generic;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;

namespace Hunter.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.All();
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
