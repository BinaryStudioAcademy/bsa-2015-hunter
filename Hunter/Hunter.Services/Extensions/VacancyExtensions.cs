using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;


namespace Hunter.Services
{
    public static class VacancyExtersion
    {
        public static VacancyDto ToVacancyDTO(this Vacancy vacancy)
        {
            var v = new VacancyDto
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Status = vacancy.Status,
                StartDate = vacancy.StartDate,
                EndDate = vacancy.EndDate,
                Description = vacancy.Description,
                PoolId = vacancy.PoolId,
                StatusName = ((Status)vacancy.Status).ToString()
            };
            return v;
        }
        public static Vacancy ToVacancy(this VacancyDto vacancy)
        {
            var v = new Vacancy
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Status = vacancy.Status,
                StartDate = vacancy.StartDate,
                EndDate = vacancy.EndDate,
                Description = vacancy.Description,
                PoolId = vacancy.PoolId
                
            };
            return v;
        }
    }

}
