﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services
{
    public interface IVacancyService
    {
        IEnumerable<VacancyDto> Get(VacancyFilterParams filterParams);
        IEnumerable<VacancyDto> Get();
        VacancyDto Get(int id);
        void Add(VacancyDto entity);
        void Update(VacancyDto entity);
        void Delete(int id);
        VacancyLongListDto GetLongList(int id);
    }
}
