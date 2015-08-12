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
        public byte[] Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
    }
}
