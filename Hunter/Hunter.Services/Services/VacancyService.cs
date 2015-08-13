using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface.Base;
using System.Reflection;
using Hunter.DataAccess.Entities;
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
        public IEnumerable<VacancyDto> Get(VacancyFilterParams filterParams)
        {
            var vacancies = _vacancyRepository.All();

            if (filterParams.Pools.Count() > 0)
                vacancies = from v in vacancies
                            where filterParams.Pools.Contains(v.PoolId.ToString())
                            select v;
            if (filterParams.Statuses.Count() > 0)
                vacancies = from v in vacancies
                            where filterParams.Statuses.Contains(v.Status.ToString())
                            select v;
            if (filterParams.AddedByArray.Count() > 0)
                vacancies = from v in vacancies
                            where filterParams.AddedByArray.Contains(v.User.Login)
                            select v;
            if (filterParams.Filter != string.Empty)
                vacancies = from v in vacancies
                            where v.Name.ToLower().Contains(filterParams.Filter.ToLower())
                            select v;
            if (filterParams.SortColumn != string.Empty)
            {
                var f = typeof(Vacancy).GetProperty(filterParams.SortColumn, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (f != null)
                    vacancies = !filterParams.ReverceSort ? vacancies.OrderBy(v => f.GetValue(v)) : vacancies.OrderByDescending(v => f.GetValue(v));
            }
            else
                vacancies = vacancies.OrderByDescending(v => v.StartDate);

            vacancies = vacancies.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);
            return vacancies.Select(item => item.ToVacancyDTO()).ToList();
        }
        public VacancyDto Get(int id)
        {
            var vacancy = _vacancyRepository.Get(id);
            if (vacancy != null)
                return vacancy.ToVacancyDTO();
            return null;
        }
        public void Add(VacancyDto entity)
        {
            if (entity == null) return;
            _vacancyRepository.Add(entity.ToVacancy());
            _unitOfWork.SaveChanges();
        }
        public void Update(VacancyDto entity)
        {
            _vacancyRepository.Update(entity.ToVacancy());
            _unitOfWork.SaveChanges();
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