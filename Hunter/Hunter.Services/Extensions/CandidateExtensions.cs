using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
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
                Salary = candidate.Salary,
                YearsOfExperience = candidate.YearsOfExperience,
                ResumeId = candidate.ResumeId,
                AddedByProfileId = candidate.AddedByProfileId,
                Cards = candidate.Card.ToList(),
                Pools = candidate.Pool.ToList(),
                Photo = candidate.Photo,
                Resolution = (int)candidate.Resolution,
                ShortListed = candidate.Shortlisted,
                Origin = (int)candidate.Origin,
                DateOfBirth = candidate.DateOfBirth
            };
            return dto;
        }

        static public Candidate ToCandidateModel(this CandidateDto dto)
        {
            var model = new Candidate()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                CurrentPosition = dto.CurrentPosition,
                Company = dto.Company,
                Location = dto.Location,
                Skype = dto.Skype,
                Phone = dto.Phone,
                Salary = dto.Salary,
                YearsOfExperience = dto.YearsOfExperience,
                ResumeId = dto.ResumeId,
                AddedByProfileId = dto.AddedByProfileId,
                Card = dto.Cards.ToList(),
                Pool = dto.Pools.ToList(),
                Photo = dto.Photo,
                Resolution = (Resolution)dto.Resolution,
                Shortlisted = dto.ShortListed,
                Origin = (Origin)dto.Origin,
                DateOfBirth = dto.DateOfBirth
            };
            return model;
        }
    }
}
