using System.Collections.Generic;
using System;
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
                AddedByName = vacancy.User != null ? vacancy.User.UserName : string.Empty,
                AddedById = vacancy.User != null ? vacancy.User.Id : 0
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

        //public static IEnumerable<AddedByDto> ToLongListAddedByDto(this IEnumerable<Card> cards)
        //{
        //    return cards.OrderBy(c=>c.AddedByProfileId).Select(c => new AddedByDto()
        //    {
        //        VacancyId = c.VacancyId,
        //        UserLogin = c.UserProfile != null ? c.UserProfile.UserLogin : "",
        //        Alias = c.UserProfile != null ? c.UserProfile.Alias : "",
        //        CountOfAddedCandidates = cards.GroupBy(cc=>cc.AddedByProfileId).Count()
        //    });
            
        //}
    }
}
