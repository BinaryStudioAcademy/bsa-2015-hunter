﻿using System.Collections.Generic;
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
        void Add(CandidateDto candidate);
        void Delete(Candidate candidate);
        void Update(CandidateDto candidate);
        IEnumerable<CandidateLongListDto> GetLongList(int id);
        CandidateLongListDetailsDto GetLongListDetails(int id);
    }
}
