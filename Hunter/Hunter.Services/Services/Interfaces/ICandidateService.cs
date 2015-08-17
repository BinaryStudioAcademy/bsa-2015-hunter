using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Interfaces
{
    public interface ICandidateService
    {
        Candidate Get(int id);
        Candidate Get(Func<Candidate, bool> predicat);
        IQueryable<Candidate> Query();
        IEnumerable<CandidateDto> GetAllInfo();
        CandidateDto GetInfo(int id);
        void Add(CandidateDto candidate);
        void Delete(Candidate candidate);
        void Update(CandidateDto candidate);
        IEnumerable<CandidateLongListDto> GetLongList(int id);
        CandidateLongListDetailsDto GetLongListDetails(int id);
    }
}
