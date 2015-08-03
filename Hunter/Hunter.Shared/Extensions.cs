using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;

namespace Hunter.Shared
{
    static public class Extensions
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
                Photo = candidate.Photo
            };
            return dto;
        }
    }
}
