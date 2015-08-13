using System.Collections.Generic;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;

namespace Hunter.Services.Interfaces
{
    public interface IUserProfileService
    {
        IList<UserProfileRowVm> LoadPage(int page);
        ResourceApiResult<EditUserProfileVm> GetById(long userProfileId);
        EditUserProfileVm GetByLogin(long userLogin);
        ApiResult Save(EditUserProfileVm userProfile);
        IdApiResult Delete(long userProfileId);
    }
}
