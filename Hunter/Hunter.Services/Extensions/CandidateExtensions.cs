using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Entities;

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
                Salary = candidate.Salary,
                YearsOfExperience = candidate.YearsOfExperience,
                ResumeId = candidate.ResumeId,
                AddedByProfileId = candidate.AddedByProfileId,
                Cards = candidate.Card.ToList(),
                Pools = candidate.Pool.ToList(),
                Photo = candidate.Photo,
                Resolution = candidate.Resolution,
                ShortListed = candidate.Shortlisted,
                Origin = candidate.Origin
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
                Resolution = dto.Resolution,
                Shortlisted = dto.ShortListed,
                Origin = dto.Origin
            };
            return model;
        }
    }
}
