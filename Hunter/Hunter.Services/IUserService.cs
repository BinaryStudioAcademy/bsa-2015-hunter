using System.Collections.Generic;
using Hunter.DataAccess.Db;

namespace Hunter.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string name);
        User GetUserById(int id);
        void CreateUser(User user);
        void AddToRole(User user, string roleName);
        void DeleteUser(User user);
        void UpdateUser(User user);
    }
}