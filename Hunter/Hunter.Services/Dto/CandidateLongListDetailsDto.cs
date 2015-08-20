using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Dto
{
    public class CandidateLongListDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? Salary { get; set; }
        public string Resolution { get; set; }
        public string Stage { get; set; }
        public string TestComment { get; set; }
        public string SpecialNotes { get; set; }
        public string PhotoUrl { get; set; }
        public bool Shortlisted { get; set; }
    }
}
