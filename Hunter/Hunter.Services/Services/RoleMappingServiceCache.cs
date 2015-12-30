using Hunter.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities.Entites;
using Hunter.Services.Dto;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class RoleMappingServiceCache : IRoleMappingServiceCache
    {
        private Dictionary<string, string> RoleMap;

        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IUserRoleRepository _roleRepository;
        private readonly ILogger _logger;
        public RoleMappingServiceCache(IRoleMappingRepository roleMappingRepository, IUserRoleRepository userRoleRepository, ILogger logger)
        {
            _roleMappingRepository = roleMappingRepository;
            _roleRepository = userRoleRepository;
            _logger = logger;
            RoleMap = new Dictionary<string, string>();
            InitializeMap();
        }

        public void InitializeMap()
        {
            RoleMap.Clear();
            try
            {
                var data = _roleMappingRepository.Query().ToList();
                data.ForEach(d => RoleMap.Add(d.Position, d.UserRole.Name));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public IEnumerable<RoleMappingDto> GetRoleMap()
        {
            try
            {
                var data = RoleMap.Select(x => new RoleMappingDto() { Position = x.Key, RoleName = x.Value });
                return data;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public string TransformRole(string authRole)
        {
            var hunterRole = RoleMap.FirstOrDefault(x => x.Key == authRole);
            if (hunterRole.Key != null)
            {
                return hunterRole.Value;
            }

            InitializeMap();
            hunterRole = RoleMap.FirstOrDefault(x => x.Key == authRole);
            if (hunterRole.Key != null)
            {
                return hunterRole.Value;
            }
            var roleDefault = _roleRepository.Get(x => x.Name == Roles.Default.ToString());
            try
            {
                _roleMappingRepository.UpdateAndCommit(new RoleMapping()
                {
                    Position = authRole,
                    RoleId = roleDefault.Id
                });
                InitializeMap();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return roleDefault.Name;
        }

        //private KeyValuePair<string, string> CheckIfExict(string authRole)
        //{
        //    var hunterRole = RoleMap.FirstOrDefault(x => x.Key == authRole);
        //    if (hunterRole.Key != null)
        //    {
        //        return hunterRole;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
