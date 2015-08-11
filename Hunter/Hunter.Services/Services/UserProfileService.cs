using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUserProfileRepository repository, IUnitOfWork unitOfWork)
        {
            _userProfileRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public UserProfile GetUserProfile(string userName)
        {
            return _userProfileRepository.Get(x => x.UserLogin.ToLower() == userName.ToLower());
        }

        public UserProfile GetUserProfile(int userId)
        {
            return _userProfileRepository.Get(userId);
        }

        public void UpdateUserProfile(UserProfile newProfile)
        {
            _userProfileRepository.Update(newProfile);
            _unitOfWork.SaveChanges();
        }
    }
}
