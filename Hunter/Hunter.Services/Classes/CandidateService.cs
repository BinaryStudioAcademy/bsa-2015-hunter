using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.Shared;

namespace Hunter.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
        }

        public IEnumerable<Candidate> GetAll()
        {
            return _candidateRepository.All();
        }

        public IEnumerable<CandidateDto> GetAllInfo()
        {
            return _candidateRepository.All().Select(x=>x.ToCandidateDto());
        }

        public Candidate Get(int id)
        {
            return _candidateRepository.Get(id);
        }

        public CandidateDto GetInfo(int id)
        {
            var candidate = _candidateRepository.Get(id);
            if (candidate == null)
            {
                return null;
            }
            return candidate.ToCandidateDto();
        }

        public void Add(Candidate candidate)
        {
            _candidateRepository.Add(candidate);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Candidate candidate)
        {
            _candidateRepository.Delete(candidate);
            _unitOfWork.SaveChanges();
        }

        public void Update(Candidate candidate)
        {
            _candidateRepository.Update(candidate);
            _unitOfWork.SaveChanges();
        }
    }
}
