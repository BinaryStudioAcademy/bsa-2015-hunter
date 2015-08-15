using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.DataAccess.Interface.Repositories;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TestService(ITestRepository testRepository, ICardRepository cardRepository, 
            IUnitOfWork unitOfWork,ILogger logger)
        {
            _testRepository = testRepository;
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<TestDto> GetAllCandidatesTests(int candidateId)
        {
            try
            {
                var tests = _testRepository
                    .Query()
                    .Where(x => x.Card.CandidateId == candidateId)
                    .Select(x => x.ToTestDto());

                return tests;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }

        public TestDto GetCandidateTest(int vacancyId, int candidateId)
        {
            try
            {
                int cardId = findCardId(vacancyId, candidateId);

                var test = _testRepository.Get(x => x.CardId == cardId);

                if (test == null)
                {
                    throw new Exception("Test not found");
                }

                return test.ToTestDto();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }

        public int AddTest(TestDto newTestDto)
        {
            Test test = new Test();
            newTestDto.ToTest(test);

            try
            {
                _testRepository.UpdateAndCommit(test);
                _unitOfWork.SaveChanges();

                return test.Id;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }

        public void UpdateTest(TestDto newTestDto)
        {
            Test test = new Test();
            newTestDto.ToTest(test);

            try
            {
                _testRepository.Update(test);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }

        public void DeleteTestById(int testId)
        {
            try
            {
                Test test = _testRepository.Get((long) testId);
                _testRepository.Delete(test);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw ex;
            }
        }

        private int findCardId(int vacancyId, int candidateId)
        {
           return _cardRepository
                .Query()
                .Where(x => x.VacancyId == vacancyId && x.CandidateId == candidateId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }
    }
}
