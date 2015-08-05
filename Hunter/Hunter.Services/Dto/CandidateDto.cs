using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Entities;

namespace Hunter.Services
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public byte[] Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CurrentPosition { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Skype { get; set; }

        public string Phone { get; set; }

        public string Salary { get; set; }

        public double? YearsOfExperience { get; set; }

        public int ResumeId { get; set; }

        public int? AddedByProfileId { get; set; }

        public List<Card> Cards { get; set; }

        public List<Pool> Pools { get; set; }

        public int Origin { get; set; }

        public int Resolution { get; set; }

        public bool ShortListed { get; set; }
    }
}
