using System.Collections.Generic;
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

        public static VacancyRowDto ToRowDto(this Vacancy vacancy)
        {
            var v = new VacancyRowDto
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Status = vacancy.Status,
                StartDate = vacancy.StartDate,
                EndDate = vacancy.EndDate,
                PoolName = vacancy.Pool.Name,
                AddedByName = vacancy.User.UserName,
                AddedById = vacancy.User.Id
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
                AddedByName = vacancy.User != null ? vacancy.User.Login : "",
                //PoolId = vacancy.PoolId
            };
            return vll;
        }

        public static IEnumerable<LongListAddedByDto> ToLongListAddedByDto(this IEnumerable<Card> cards)
        {
            return cards.OrderBy(c=>c.AddedByProfileId).Select(c => new LongListAddedByDto()
            {
                VacancyId = c.VacancyId,
                UserLogin = c.UserProfile != null ? c.UserProfile.UserLogin : "",
                Alias = c.UserProfile != null ? c.UserProfile.Alias : "",
                CountOfAddedCandidates = cards.GroupBy(cc=>cc.AddedByProfileId).Count()
            });
            //return cards.Select(c => new CandidateLongListDto()
            //{
            //    Id = c.CandidateId,
            //    FirstName = c.Candidate != null ? c.Candidate.FirstName : "",
            //    LastName = c.Candidate != null ? c.Candidate.LastName : "",
            //    Email = c.Candidate != null ? c.Candidate.Email : "",
            //    Company = c.Candidate != null ? c.Candidate.Company : "",
            //    Location = c.Candidate != null ? c.Candidate.Location : "",
            //    Salary = c.Candidate != null ? c.Candidate.Salary : 0,
            //    YearsOfExperience = c.Candidate != null ? c.Candidate.YearsOfExperience : 0,
            //    Stage = c.Stage,
            //    Resolution = c.Candidate != null ? c.Candidate.Resolution.ToString() : "",
            //    AddedBy = c.UserProfile != null ? c.UserProfile.UserLogin : "",
            //    AddDate = c.Added,
            //    PhotoUrl = "api/fileupload/pictures/" + c.Id,
            //    Shortlisted = c.Candidate != null && c.Candidate.Shortlisted
            //});
        }
    }
}
