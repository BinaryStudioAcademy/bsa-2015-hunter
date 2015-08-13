using System.Collections.Generic;
using Hunter.DataAccess.Entities;

namespace Hunter.Services.Services.Interfaces
{
    public interface IUserProfileService
    {
        IEnumerable<UserProfile> GetAllUsersProfiles();
        UserProfile GetUserProfile(string userName);
        UserProfile GetUserProfile(int userId);
        void UpdateUserProfile(UserProfile newProfile);
        void AddUserProfile(UserProfile profile);
        void DeleteUserProfile(int id);
    }
}