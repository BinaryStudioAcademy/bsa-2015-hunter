using System.Collections.Generic;

namespace Hunter.Services.Interfaces
{
    public interface IVacancyService
    {
        IEnumerable<VacancyDto> Get(VacancyFilterParams filterParams);
        IEnumerable<VacancyDto> Get();
        VacancyDto Get(int id);
        void Add(VacancyDto dto);
        void Update(VacancyDto entity);
        void Delete(int id);
        VacancyLongListDto GetLongList(int id);
    }
}
