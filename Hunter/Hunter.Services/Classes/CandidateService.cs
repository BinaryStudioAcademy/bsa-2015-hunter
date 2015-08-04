using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.Shared;

namespace Hunter.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger _logger ;

        public CandidateService(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
            _logger = logger;
        }

        public IEnumerable<Candidate> GetAll()
        {
            try
            {
                return _candidateRepository.All();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public IEnumerable<CandidateDto> GetAllInfo()
        { 
            try
            {
                return _candidateRepository.All().Select(x => x.ToCandidateDto());
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public Candidate Get(int id)
        {
            try
            {
                return _candidateRepository.Get(id);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public CandidateDto GetInfo(int id)
        {
            try
            {
                var candidate = _candidateRepository.Get(id);
                if (candidate == null)
                {
                    return null;
                }
                return candidate.ToCandidateDto();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }

        }

        public void Add(Candidate candidate)
        {
            try
            {
                _candidateRepository.Add(candidate);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public void Delete(Candidate candidate)
        {
            try
            {
                _candidateRepository.Delete(candidate);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public void Update(Candidate candidate)
        {
            try
            {
                _candidateRepository.Update(candidate);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
