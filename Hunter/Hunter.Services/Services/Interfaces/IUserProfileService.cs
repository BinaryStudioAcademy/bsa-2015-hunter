using Hunter.DataAccess.Entities;

namespace Hunter.Services.Services.Interfaces
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfile(string userName);
        UserProfile GetUserProfile(int userId);
        void UpdateUserProfile(UserProfile newProfile);
    }
}