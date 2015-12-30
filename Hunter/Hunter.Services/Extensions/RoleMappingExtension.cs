using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities.Entites;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class RoleMappingExtension
    {
        public static IEnumerable<RoleMappingDto> ToUseRoleMappingDtos(this IEnumerable<RoleMapping> roleMapping)
        {
            return roleMapping.Select(r => new RoleMappingDto()
            {
                Id = r.Id,
                RoleId = r.RoleId,
                Position = r.Position,
                RoleName = r.UserRole.Name
            });
        }

        public static RoleMappingDto ToRoleMappingDto(this RoleMapping roleMapping)
        {
            return new RoleMappingDto()
            {
                Id = roleMapping.Id,
                RoleId = roleMapping.RoleId,
                Position = roleMapping.Position,
                RoleName = roleMapping.UserRole.Name
            };
        }

        public static RoleMapping ToRoleMapping(this RoleMappingDto roleMappingDto)
        {
            return new RoleMapping()
            {
                Id = roleMappingDto.Id,
                RoleId = roleMappingDto.RoleId,
                Position = roleMappingDto.Position
            };
        }
    }
}
