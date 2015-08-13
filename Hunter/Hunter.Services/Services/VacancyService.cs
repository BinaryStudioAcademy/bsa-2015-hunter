using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface.Base;
using System;

namespace Hunter.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ICandidateRepository _candidateRepository;
        //private readonly ICardRepository _cardRepository;
        private readonly ILogger _logger;

        public VacancyService(
            IVacancyRepository vacancyRepository,
            ICandidateRepository candidateRepository,
            //ICardRepository cardRepository,
            ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _vacancyRepository = vacancyRepository;
            _candidateRepository = candidateRepository;
            //_cardRepository = cardRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<VacancyDto> Get()
        {
            var vacancies = _vacancyRepository.All();
            return vacancies.Select(item => item.ToVacancyDTO()).ToList();
        }
        public VacancyDto Get(int id)
        {
            var vacancy = _vacancyRepository.Get(id);
            if (vacancy != null)
                return vacancy.ToVacancyDTO();
            return null;
        }
        public void Add(VacancyDto dto)
        {
            if (dto == null) return;
            _vacancyRepository.UpdateAndCommit(dto.ToVacancy());
        }
        public void Update(VacancyDto entity)
        {
            _vacancyRepository.UpdateAndCommit(entity.ToVacancy());
        }
        public void Delete(int id)
        {
            var vacancy = _vacancyRepository.Get(id);
            if (vacancy != null)
            {
                _vacancyRepository.Delete(vacancy);
                _unitOfWork.SaveChanges();
            }
        }

        public VacancyLongListDto GetLongList(int id)
        {
            try
            {
                var vacancyLongList = _vacancyRepository.Get(id).ToVacancyLongListDto();

                return vacancyLongList;

                //var candidates = _candidateRepository.All().Where(c => c.Card.Any(card=>card.VacancyId==id));

                
                
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new VacancyLongListDto();
            }
        }
    }
}