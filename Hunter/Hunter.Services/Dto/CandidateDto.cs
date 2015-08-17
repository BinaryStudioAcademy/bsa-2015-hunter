using System;
using System.Collections.Generic;
using Hunter.DataAccess.Entities;

namespace Hunter.Services.Dto
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CurrentPosition { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Skype { get; set; }

        public string Phone { get; set; }

        public string Linkedin { get; set; }

        public double? Salary { get; set; }

        public double? YearsOfExperience { get; set; }

        public int ResumeId { get; set; }

        public int? AddedByProfileId { get; set; }

        public string AddedBy { get; set; }

        public DateTime AddDate { get; set; }

        public List<CardDto> Cards { get; set; }

        public List<string> PoolNames { get; set; }

        public int Origin { get; set; }

        public int Resolution { get; set; }

        public bool ShortListed { get; set; }

        public DateTime DateOfBirth {get; set; } 

        public string PhotoUrl { get; set; }
    }
}
