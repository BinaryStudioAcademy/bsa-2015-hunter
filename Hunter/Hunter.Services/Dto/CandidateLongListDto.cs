using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services
{
    public class CandidateLongListDto
    {
        public int Id { get; set; }

        public byte[] Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Salary { get; set; }

        public double? YearsOfExperience { get; set; }
    }
}
