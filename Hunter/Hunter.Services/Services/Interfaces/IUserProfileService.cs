using System.Collections.Generic;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;

namespace Hunter.Services.Interfaces
{
    public interface IUserProfileService
    {
        IList<UserProfileRowVm> LoadPage(int page);
        IEnumerable<UserProfileRowVm> GetUserProfiles(string roleName);
        ResourceApiResult<EditUserProfileVm> GetById(long userProfileId);
        ApiResult Save(EditUserProfileVm userProfile);
        IdApiResult Delete(long userProfileId);
        bool UserExist(string userName);
    }
}
