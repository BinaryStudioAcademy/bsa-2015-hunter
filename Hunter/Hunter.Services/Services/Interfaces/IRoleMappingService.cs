﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities.Entites;
using Hunter.Services.Dto;

namespace Hunter.Services.Services.Interfaces
{
    public interface IRoleMappingService
    {
        IEnumerable<RoleMapping> GetAllInfo();
        void Update(RoleMapping roleMapping);
    }
}
