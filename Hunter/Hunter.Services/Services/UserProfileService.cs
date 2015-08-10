using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Concrete;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class UserProfileService : IUserProfileService
    {
        private const int _ItemsPerPage = 15;
        private readonly IUserProfileRepository _profileRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UserProfileService(IUserProfileRepository profileRepo, IUnitOfWork unitOfWork, ILogger logger)
        {
            _profileRepo = profileRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IList<UserProfileRowVm> LoadPage(int page)
        {
            if (page <= 0)
                page = 1;
            var skip = (page - 1) * _ItemsPerPage;

            var res = _profileRepo.Query()
                .Where(pr => !pr.IsDeleted)
                .OrderBy(p => p.Added)
                .Skip(skip).Take(_ItemsPerPage)
                .ToList()
                .Select(UserProfileRowVm.Create).ToList();
            return res;
        }

        public ResourceApiResult<EditUserProfileVm> GetById(long userProfileId)
        {
            if (userProfileId <= 0)
                return Api.ResourceNotFound<EditUserProfileVm>(userProfileId);

            var profile = _profileRepo.Get(userProfileId);

            if (profile == null || profile.IsDeleted)
                return Api.ResourceNotFound<EditUserProfileVm>(userProfileId);

            return Api.Details(userProfileId, EditUserProfileVm.Create(profile));
        }

        public ApiResult Save(EditUserProfileVm editedUserProfile)
        {
            var profile = _profileRepo.Get(editedUserProfile.Id) ?? new UserProfile();
            if (profile.IsNew())
            {
                var same = _profileRepo.Get(pr => pr.UserLogin == editedUserProfile.Login);
                if (same != null)
                    return Api.Conflict(string.Format("Profile with e-mail {0} already exists", editedUserProfile.Login));
            }
            editedUserProfile.Map(profile, _unitOfWork);
            if (editedUserProfile.Id == 0)
            {
                profile.Added = DateTime.UtcNow;
            }
            _profileRepo.UpdateAndCommit(profile);
            return editedUserProfile.Id == 0 ? Api.Added(profile.Id, EditUserProfileVm.Create(profile)) : Api.Updated(profile.Id);
        }

        public IdApiResult Delete(long userProfileId)
        {
            try
            {
                var entity = _profileRepo.Get(userProfileId);
                if (entity == null || entity.IsDeleted)
                    return Api.NotFound(userProfileId);

                _profileRepo.DeleteAndCommit(entity);
                return Api.Deleted(userProfileId);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                return Api.Error(userProfileId, ex.Message);
            }
        }
    }
}
