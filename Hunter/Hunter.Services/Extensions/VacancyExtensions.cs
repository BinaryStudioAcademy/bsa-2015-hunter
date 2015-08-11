using Hunter.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CountCandidates = vacancy.Card.Count(),
                CountShortListed = vacancy.Card.Where(e=>e.Candidate.Shortlisted == true).Count(),
                AddedByName = vacancy.User != null ? vacancy.User.Login : "noname"
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

        public static VacancyLongListDto ToVacancyLongListDto(this Vacancy vacancy)
        {
            var vll = new VacancyLongListDto()
            {
               
                Id = vacancy.Id,
                Name = vacancy.Name,
                PoolId = vacancy.PoolId,
                AddedByName = vacancy.User != null ? vacancy.User.Login : "noname",
                CandidateLongListDto = null
            };
            return vll;
        }
    }
}
