using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities.Entites;
using Hunter.DataAccess.Interface;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class UserRoleMappingService : IUserRoleMappingService
    {
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly ILogger _logger;
        public UserRoleMappingService(IRoleMappingRepository roleMappingRepository, ILogger logger)
        {
            _roleMappingRepository = roleMappingRepository;
            _logger = logger;
        }

        public IEnumerable<RoleMapping> GetAllInfo()
        {
            try
            {
                var data = _roleMappingRepository.Query().ToList();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public void Update(RoleMapping roleMapping)
        {
            var roleMap = _roleMappingRepository.Get(roleMapping.Id);
            roleMap.RoleId = roleMapping.RoleId;
            try
            {
                _roleMappingRepository.UpdateAndCommit(roleMap);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
