using System.Collections.Generic;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Interfaces
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
