﻿using System.Collections.Generic;
using Hunter.Services.Dto;
using Hunter.Services.Dto.ApiResults;

namespace Hunter.Services.Services.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetAllCandidatesTests(int candidateId);
        int AddTest(TestDto newTestDto);
        void UpdateTest(TestDto newTestDto);
        void DeleteTestById(int testId);
        TestsResult GetCardTests(int vacancyId, int candidateId);
    }
}