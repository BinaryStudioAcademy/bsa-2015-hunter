using System.Collections.Generic;
using System.Threading.Tasks;
using Hunter.Services.Dto;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;

namespace Hunter.Services.Interfaces
{
    public interface IUserProfileService
    {
        IList<UserProfileRowVm> LoadPage(int page);
        IEnumerable<UserProfileDto> GetUserProfilesByRole(string roleName);
        IEnumerable<string> GetAuthUserIdByRole(string roleName);
        ResourceApiResult<EditUserProfileVm> GetById(long userProfileId);
        ApiResult Save(EditUserProfileVm userProfile);
        ApiResult Update(EditUserProfileVm userProfile);
        IdApiResult Delete(long userProfileId);
        bool UserExist(string userName);
        Task<OAuthUserDto> CreateUserAlias(string url);
    }
}
