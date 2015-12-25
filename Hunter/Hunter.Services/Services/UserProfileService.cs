using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Hunter.Common.Concrete;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Dto.ApiResults;
using Hunter.Services.Dto.User;
using Hunter.Services.Interfaces;
using Newtonsoft.Json;

namespace Hunter.Services
{
    public class AccountService
    {
        public AccountService(
            //IUnitOfWork unitOfWork,
            IUserRoleRepository roleRepository)
        {
            //_unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        private readonly IUserRoleRepository _roleRepository;
        private readonly IUserProfileRepository _profileRepo;

        //todo new fix figure out

        public IEnumerable<UserProfileRowVm> GetUserProfiles(string roleName)
        {
            //var users = _roleRepository
            //    .Query().FirstOrDefault(e => e.Name == roleName)
            //    .Users
            //    .Select(u => u.Login);

            //var profiles = _profileRepo.Query()
            //                     .Where(pr => users.Contains(pr.UserLogin))
            //                     .Select(UserProfileRowVm.Create);
            //return profiles;
            return null;
        }

        public bool UserExist(string userName)
        {
            return _profileRepo.Query().Any(p => p.UserLogin.ToLower() == userName.ToLower());
        }
    }

    public class UserProfileService : IUserProfileService
    {
        private const int _ItemsPerPage = 15;
        private readonly IActivityHelperService _activityHelperService;
        private readonly IUserProfileRepository _profileRepo;
        private readonly IUserRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(
            IActivityHelperService activityHelperService,
            IUserProfileRepository profileRepo,
            IUnitOfWork unitOfWork,
            IUserRoleRepository roleRepository)
        {
            _activityHelperService = activityHelperService;
            _profileRepo = profileRepo;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        //todo new figure out

        public IEnumerable<UserProfileRowVm> GetUserProfiles(string roleName)
        {
            //var users = _roleRepository
            //    .Query()
            //    .Where(e => e.Name == roleName)
            //    .FirstOrDefault()
            //    .Users
            //    .Select(e => UserProfileRowVm.Create(_profileRepo.Get(e.Id)));
            //return users;
            return null;
        }

        public bool UserExist(string userName)
        {
            return _profileRepo.Query().Any(p => p.UserLogin.ToLower() == userName.ToLower());
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
            if (string.IsNullOrEmpty(editedUserProfile.Login))
                return Api.Conflict("Login is required to be a valid e-mail");

            var profile = _profileRepo.Get(editedUserProfile.Id) ?? new UserProfile();

            var same = _profileRepo.Get(pr => pr.UserLogin == editedUserProfile.Login);
            if (same != null && same.Id != profile.Id)
                return Api.Conflict(string.Format("Profile with e-mail {0} already exists", editedUserProfile.Login));

            same = _profileRepo.Get(pr => pr.Alias == editedUserProfile.Alias);
            if (same != null && same.Id != profile.Id)
                return Api.Conflict(string.Format("Profile with alias {0} already exists", editedUserProfile.Alias));

            editedUserProfile.Map(profile, _unitOfWork);
            if (profile.IsNew())
            {
                profile.Added = DateTime.UtcNow;
            }
            try
            {
                _profileRepo.UpdateAndCommit(profile);
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine("Exception:", ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (editedUserProfile.Id == 0)
            {
                _activityHelperService.CreateAddedUserProfileActivity(profile);
            }
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

        public async Task<OAuthUserDto> CreateUserAlias(string url)
        {
            var http = new HttpClient();

            var httpCookie = HttpContext.Current.Request.Cookies.Get("x-access-token");
            if (httpCookie == null) return null;
            var token = httpCookie.Value;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("x-access-token", token);
            var response = await http.SendAsync(requestMessage);

            var json = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<OAuthUserDto>(json);
            if (user.Name != null)
            {
                var alias = string.Empty;
                user.Name.Split(' ').ToList().ForEach(i => alias += i[0]);
                user.Name = alias;
            }
            else
            {
                user.Name = user.Email.Substring(0, user.Email.IndexOf('@'));
            }
            if (user.Name.Length > 15)
            {
                user.Name = user.Name.Substring(0, 14);
            }
            //user.Name.Split(' ').ToList().ForEach(i => alias += i[0]);
            //user.Name = alias;

            return user;
        }
    }
}
