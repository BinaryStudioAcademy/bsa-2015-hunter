using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities.Entites;
using Hunter.Services.Dto;

namespace Hunter.Services.Services.Interfaces
{
    public interface IRoleMappingServiceCache
    {
        IEnumerable<RoleMappingDto> GetRoleMap();
        string TransformRole(string authRole);
        void InitializeMap();
    }
}
