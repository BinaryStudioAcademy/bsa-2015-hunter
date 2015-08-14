using System.Collections.Generic;

namespace Hunter.Services.Interfaces
{
    public interface IVacancyService
    {
        IList<VacancyRowDto> Get();
        VacancyDto Get(int id);
        void Add(VacancyDto dto);
        void Update(VacancyDto entity);
        void Delete(int id);
        VacancyLongListDto GetLongList(int id);
    }
}
