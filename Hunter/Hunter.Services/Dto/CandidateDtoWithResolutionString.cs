using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities.Enums;

namespace Hunter.Services.Dto
{
    public class CandidateDtoWithResolutionString : CandidateDto
    {
        public string ResolutionString { get; set; }

        public CandidateDtoWithResolutionString(CandidateDto candidate)
        {
            Id = candidate.Id;
            FirstName = candidate.FirstName;
            LastName = candidate.LastName;
            Email = candidate.Email;
            CurrentPosition = candidate.CurrentPosition;
            Company = candidate.Company;
            Location = candidate.Location;
            Skype = candidate.Skype;
            Phone = candidate.Phone;
            Linkedin = candidate.Linkedin;
            Salary = candidate.Salary;
            YearsOfExperience = candidate.YearsOfExperience;
            ResumeId = candidate.ResumeId;
            AddedByProfileId = candidate.AddedByProfileId;
            Cards = candidate.Cards;
            PoolNames = candidate.PoolNames;
            Resolution = candidate.Resolution;
            ShortListed = candidate.ShortListed;
            Origin = candidate.Origin;
            DateOfBirth = candidate.DateOfBirth;

            ResolutionString = Enum.GetName(typeof (Resolution), (DataAccess.Entities.Enums.Resolution)Resolution);
        }
    }
}
