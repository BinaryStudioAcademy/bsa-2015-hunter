using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface.Base;
using System.Reflection;
using Hunter.DataAccess.Entities;
using System;
using System.Globalization;
using Hunter.Services.Interfaces;

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
        public IList<VacancyRowDto> Get()
        {
            var vacancies = _vacancyRepository.QueryIncluding(v => v.Pool).OrderByDescending(v => v.StartDate).ToList();
            return vacancies.Select(item => item.ToRowDto()).ToList();
        }
        public PageDto<VacancyRowDto> Get(VacancyFilterParams filterParams)
        {
            IQueryable<Vacancy> query = _vacancyRepository.QueryIncluding(v => v.Pool, v => v.User);

            if (filterParams.Pools.Any())
            {
                var selectedPools = filterParams.Pools.Select(Int32.Parse).ToList();
                query = query.Where(vac => selectedPools.Contains(vac.PoolId));
            }

            if (filterParams.Statuses.Any())
            {
                var selectedStatuses = filterParams.Statuses.Select(Int32.Parse).ToList();
                query = query.Where(vac => selectedStatuses.Contains(vac.Status));
            }
            if (filterParams.Statuses.Any())
            {
                var selectedStatuses = filterParams.Statuses.Select(Int32.Parse).ToList();
                query = query.Where(vac => selectedStatuses.Contains(vac.Status));
            }

            if (filterParams.AddedByArray.Any())
            {
                var selectedCreators = filterParams.AddedByArray;
                query = query.Where(vac => selectedCreators.Contains(vac.User.Login));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.Filter))
            {
                var nameFilter = filterParams.Filter.ToLowerInvariant();
                query = query.Where(vac => vac.Name.Contains(nameFilter));
            }
            IOrderedQueryable<Vacancy> orderedQuery = null;
            if (filterParams.SortColumn == "startDate")
            {
                orderedQuery = filterParams.ReverceSort ? query.OrderByDescending(v => v.StartDate) : query.OrderBy(v => v.StartDate);
            }
            else if (filterParams.SortColumn == "name")
            {
                orderedQuery = filterParams.ReverceSort ? query.OrderByDescending(v => v.Name) : query.OrderBy(v => v.Name);
            }
            else
            {
                orderedQuery = query.OrderByDescending(v => v.StartDate);
            }

            var countRecords = query.Count();

            var vacanciesFiltered = orderedQuery.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize).ToList();

            var list = vacanciesFiltered.Select(item => item.ToRowDto()).ToList();
            return new PageDto<VacancyRowDto>
            {
                TotalCount = countRecords,
                Rows = list
            };
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
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new VacancyLongListDto();
            }
        }

        public IEnumerable<LongListAddedByDto> GetLongListAddedBy(int vid)
        {
            try
            {


                return new List<LongListAddedByDto>();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new List<LongListAddedByDto>();
            }
        }
    }
}