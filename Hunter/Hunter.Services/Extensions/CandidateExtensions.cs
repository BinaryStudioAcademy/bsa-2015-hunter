using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;
using Hunter.Services.Dto;

namespace Hunter.Services
{
    static public class CandidateExtensions
    {
        static public CandidateDto ToCandidateDto(this Candidate candidate)
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
                Cards = candidate.Card.ToList(),
                PoolNames = candidate.Pool.Select(x=>x.Name).ToList(),
                Photo = candidate.Photo,
                Resolution = (int)candidate.Resolution,
                ShortListed = candidate.Shortlisted,
                Origin = (int)candidate.Origin,
                DateOfBirth = candidate.DateOfBirth
            };
            return dto;
        }

        static public void ToCandidateModel(this CandidateDto dto, Candidate candidate)
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
//                Card = dto.Cards.ToList();
//                Pool = new List<Pool>();
                candidate.Photo = dto.Photo;
                candidate.Resolution = (Resolution)dto.Resolution;
                candidate.Shortlisted = dto.ShortListed;
                candidate.Origin = (Origin)dto.Origin;
            candidate.DateOfBirth = dto.DateOfBirth;
        }

        public static CandidateLongListDto ToCandidateLongListDto(this Candidate candidate)
        {
            var dto = new CandidateLongListDto()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                Company = candidate.Company,
                Location = candidate.Location,
                Salary = candidate.Salary,
                YearsOfExperience = candidate.YearsOfExperience,
                Photo = candidate.Photo,
            };
            
            return dto;
        }
        
    }
}
