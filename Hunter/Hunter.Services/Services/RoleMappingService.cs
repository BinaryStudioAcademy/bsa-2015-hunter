using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities.Entites;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.DataAccess.Interface;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class RoleMappingService : IRoleMappingService
    {
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IUserRoleRepository _roleRepository;
        private readonly ILogger _logger;
        public RoleMappingService(IRoleMappingRepository roleMappingRepository, IUserRoleRepository userRoleRepository, ILogger logger)
        {
            _roleMappingRepository = roleMappingRepository;
            _roleRepository = userRoleRepository;
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

        public int AddRoleMapping(string position)
        {
            var roleMapping = _roleMappingRepository.Get(x => x.Position == position);
            if (roleMapping != null)
            {
                return roleMapping.RoleId;
            }
            var role = _roleRepository.Get(x => x.Name == Roles.TechnicalSpecialist.ToString());

            try
            {
                _roleMappingRepository.UpdateAndCommit(new RoleMapping()
                {
                    Position = position,
                    RoleId = role.Id
                });
            }
            catch (Exception ex)
            {
                return 0;
            }
            return role.Id;
        }
    }
}
