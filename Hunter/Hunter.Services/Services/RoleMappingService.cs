using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class RoleMappingService : IRoleMappingService
    {
        private Dictionary<string, string> RoleMap;

        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IUserRoleRepository _roleRepository;
        private readonly ILogger _logger;
        public RoleMappingService(IRoleMappingRepository roleMappingRepository, IUserRoleRepository userRoleRepository, ILogger logger)
        {
            _roleMappingRepository = roleMappingRepository;
            _roleRepository = userRoleRepository;
            _logger = logger;
            RoleMap = new Dictionary<string, string>();
            InitializeMap();
        }

        private void InitializeMap()
        {
            RoleMap.Clear();
            var data = _roleMappingRepository.Query().ToList();
            data.ForEach(d => RoleMap.Add(d.Position, d.UserRole.Name));
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

        public string TransferRole(string authRole)
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
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
            return roleDefault.Name;
        }



        public void Update(RoleMapping roleMapping)
        {
            var roleMap = _roleMappingRepository.Get(roleMapping.Id);
            roleMap.RoleId = roleMapping.RoleId;
            try
            {
                _roleMappingRepository.UpdateAndCommit(roleMap);
                InitializeMap();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        //public int AddRoleMapping(string position)
        //{
        //    var roleMapping = _roleMappingRepository.Get(x => x.Position == position);
        //    if (roleMapping != null)
        //    {
        //        return roleMapping.RoleId;
        //    }
        //    var role = _roleRepository.Get(x => x.Name == Roles.Default.ToString());

        //    try
        //    {
        //        _roleMappingRepository.UpdateAndCommit(new RoleMapping()
        //        {
        //            Position = position,
        //            RoleId = role.Id
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //    return role.Id;
        //}
    }
}
