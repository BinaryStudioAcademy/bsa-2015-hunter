using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Db;
using Hunter.Shared;

namespace Hunter.Services
{
    public interface ICandidateService
    {
        IEnumerable<Candidate> GetAll();
        Candidate Get(int id);
        IEnumerable<CandidateDto> GetAllInfo();
        CandidateDto GetInfo(int id);
        void Add(Candidate candidate);
        void Delete(Candidate candidate);
        void Update(Candidate candidate);
    }
}
