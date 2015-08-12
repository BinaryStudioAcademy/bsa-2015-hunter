using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;

namespace Hunter.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVacancyRepository _vacancyRepository;
        
        public VacancyService(
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork)
        {
            _vacancyRepository = vacancyRepository;
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
    }
}