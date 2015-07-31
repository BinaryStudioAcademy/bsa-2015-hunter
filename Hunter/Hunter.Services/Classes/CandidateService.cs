using System.Collections.Generic;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;


namespace Hunter.Services
{
    public class CandidateService : ICandidateService
    {
        private IUnitOfWork _unitOfWork;
        private ICandidateRepository _candidateRepository;

        public CandidateService(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
        }
        
        public IEnumerable<Candidate> GetAll()
        {
            return _candidateRepository.All();
        }

        public Candidate Get(int id)
        {
            return _candidateRepository.Get(id);
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
