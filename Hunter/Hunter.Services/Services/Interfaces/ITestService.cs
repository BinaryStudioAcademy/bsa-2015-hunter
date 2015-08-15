using System.Collections.Generic;
using Hunter.Services.Dto;

namespace Hunter.Services.Services.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetAllCandidatesTests(int candidateId);
        int AddTest(TestDto newTestDto);
        void UpdateTest(TestDto newTestDto);
        void DeleteTestById(int testId);
        TestDto GetCandidateTest(int vacancyId, int candidateID);
    }
}