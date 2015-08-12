using System;
using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger _logger ;
        private readonly IPoolRepository _poolRepository;

        public CandidateService(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository,
            IPoolRepository poolRepository, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
            _logger = logger;
            _poolRepository = poolRepository;
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
                var data = _candidateRepository.All().Select(x => x.ToCandidateDto());
                return data;
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

        public void Add(CandidateDto dto)
        {
            var candidate = new Candidate();
            dto.ToCandidateModel(candidate);
            foreach (var item in dto.PoolNames)
            {
                candidate.Pool.Add(_poolRepository.Get(x => x.Name == item));
            }
            try
            {
                candidate.AddDate = DateTime.Now;
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

        public void Update(CandidateDto dto)
        {
            var candidate = _candidateRepository.Get(dto.Id);
            dto.ToCandidateModel(candidate);
            candidate.Pool.Clear();
            foreach (var item in dto.PoolNames)
            {
                candidate.Pool.Add(_poolRepository.Get(x=>x.Name==item));
            }
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
