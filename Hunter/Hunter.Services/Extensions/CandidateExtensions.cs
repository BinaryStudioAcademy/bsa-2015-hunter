using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;
using Hunter.Services.Dto;
using System;
using System.Collections.Generic;

namespace Hunter.Services
{
    public static class CandidateExtensions
    {
        public static CandidateDto ToCandidateDto(this Candidate candidate)
        {
            var dto = new CandidateDto()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                CurrentPosition = candidate.CurrentPosition,
                Company = candidate.Company,
                Location = candidate.Location,
                Skype = candidate.Skype,
                Phone = candidate.Phone,
                Linkedin = candidate.Linkedin,
                Salary = candidate.Salary,
                YearsOfExperience = candidate.YearsOfExperience,
                ResumeId = candidate.ResumeId,
                AddedByProfileId = candidate.AddedByProfileId,
                AddedBy = candidate.AddedByProfileId.HasValue ? candidate.UserProfile.UserLogin : "",
                AddDate = candidate.AddDate,
                Cards = candidate.Card.ToList(),
                PoolNames = candidate.Pool.Select(x => x.Name).ToList(),
                Photo = candidate.Photo,
                Resolution = (int) candidate.Resolution,
                ShortListed = candidate.Shortlisted,
                Origin = (int) candidate.Origin,
                DateOfBirth = candidate.DateOfBirth
            };
            return dto;
        }

        public static void ToCandidateModel(this CandidateDto dto, Candidate candidate)
        {
            candidate.Id = dto.Id;
            candidate.FirstName = dto.FirstName;
            candidate.LastName = dto.LastName;
            candidate.Email = dto.Email;
            candidate.CurrentPosition = dto.CurrentPosition;
            candidate.Company = dto.Company;
            candidate.Location = dto.Location;
            candidate.Skype = dto.Skype;
            candidate.Phone = dto.Phone;
            candidate.Linkedin = dto.Linkedin;
            candidate.Salary = dto.Salary;
            candidate.YearsOfExperience = dto.YearsOfExperience;
            candidate.ResumeId = dto.ResumeId;
            candidate.AddedByProfileId = dto.AddedByProfileId;
            //Card = dto.Cards.ToList();
            //Pool = new List<Pool>();
            candidate.Photo = dto.Photo;
            candidate.Resolution = (Resolution)dto.Resolution;
            candidate.Shortlisted = dto.ShortListed;
            candidate.Origin = (Origin)dto.Origin;
            candidate.DateOfBirth = dto.DateOfBirth;
        }

        public static IEnumerable<CandidateLongListDto> ToCandidateLongListDto(this IEnumerable<Card> cards)
        {
            return cards.Select(c => new CandidateLongListDto()
            {
                Id = c.CandidateId,
                Photo = c.Candidate != null ? c.Candidate.Photo : new byte[64],
                FirstName = c.Candidate != null ? c.Candidate.FirstName : "",
                LastName = c.Candidate != null ? c.Candidate.LastName : "",
                Email = c.Candidate != null ? c.Candidate.Email : "",
                Company = c.Candidate != null ? c.Candidate.Company : "",
                Location = c.Candidate != null ? c.Candidate.Location : "",
                Salary = c.Candidate != null ? c.Candidate.Salary : 0,
                YearsOfExperience = c.Candidate != null ? c.Candidate.YearsOfExperience : 0,
                Stage = c.Stage,
                Resolution = c.Candidate != null ? c.Candidate.Resolution.ToString() : "",
                UserAddsCard = c.UserProfile != null ? c.UserProfile.Alias : "",
                AddDate = c.Added
            });
        }

        public static CandidateLongListDetailsDto ToCandidateLongListDetailsDto(this Candidate candidate)
        {
            return new CandidateLongListDetailsDto()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Photo = candidate.Photo,
                Salary = candidate.Salary
            };
        }
    }
}