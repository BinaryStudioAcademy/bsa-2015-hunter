﻿using System;
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
        private readonly ICardRepository _cardRepository;
        private readonly ILogger _logger;
        private readonly IPoolRepository _poolRepository;

        public CandidateService(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository, ICardRepository cardRepository,
            IPoolRepository poolRepository, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
            _cardRepository = cardRepository;
            _logger = logger;
            _poolRepository = poolRepository;
        }

        public IEnumerable<CandidateDto> GetAllInfo()
        {
            try
            {
                var data = _candidateRepository.Query().ToList().Select(x => x.ToCandidateDto());
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

        public Candidate Get(Func<Candidate, bool> predicate)
        {
            try
            {
                return _candidateRepository.Get(predicate);
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

        public IEnumerable<CandidateLongListDto> GetLongList(int id)
        {
            try
            {
                var cardsLongList = _cardRepository.QueryIncluding(c=>c.Candidate, c=>c.UserProfile)
                                                   .Where(card => card.VacancyId == id)
                                                   .ToList()
                                                   .ToDto();
                return cardsLongList;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new List<CandidateLongListDto>();
            }
        }

        public CandidateLongListDetailsDto GetLongListDetails(int id)
        {
            try
            {
                var candidate = _candidateRepository.Get(id).ToCandidateLongListDetailsDto();
                return candidate;
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
                var pool = _poolRepository.Get(x => x.Name == item);
                if (pool != null)
                {
                    candidate.Pool.Add(pool);
                }
            }
            try
            {
                candidate.AddDate = DateTime.Now;
                _candidateRepository.UpdateAndCommit(candidate);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw new Exception("Database save error!"); 
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
                var pool = _poolRepository.Get(x => x.Name == item);
                if (pool != null)
                {
                    candidate.Pool.Add(pool);
                }
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
