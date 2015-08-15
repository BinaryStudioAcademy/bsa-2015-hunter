using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class SpecialNoteService : ISpecialNoteService
    {
        private readonly ISpecialNoteRepository _specialNoteRepository;
        //private readonly IUserProfileRepository _userProfileRepository;

        public SpecialNoteService(ISpecialNoteRepository specialNoteRepository)
        {
            _specialNoteRepository = specialNoteRepository;
            //_userProfileRepository = userProfileRepository;
        }

        public IEnumerable<SpecialNoteDto> GetAllSpecialNotes()
        {
            var specialNotes = _specialNoteRepository.All();

            return specialNotes.Select(x => x.ToSpecialNoteDto()).ToList();
        }


        public SpecialNoteDto GetSpecialNoteById(int id)
        {
            return _specialNoteRepository.Get(id).ToSpecialNoteDto();
        }


        public void AddSpecialNote(SpecialNoteDto entity)
        {
            _specialNoteRepository.UpdateAndCommit(entity.ToSpecialNote());
        }

        public void UpdateSpecialNote(SpecialNoteDto entity)
        {
            _specialNoteRepository.UpdateAndCommit(entity.ToSpecialNote());
        }


        public void DeleteSpecialNoteById(int id)
        {
            var specialNote = _specialNoteRepository.Get(id);
            _specialNoteRepository.DeleteAndCommit(specialNote);
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForUser(string login)
        {
            return
                _specialNoteRepository.All()
                    .Where(x => x.UserLogin == login)
                    .Select(x => x.ToSpecialNoteDto())
                    .ToList();
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCandidate(int candidateId)
        {
            return _specialNoteRepository.All().Where(x => x.Card.CandidateId == candidateId)
                .Select(x => x.ToSpecialNoteDto()).ToList();
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCard(int vacancyId, int candidateId)
        {
            return _specialNoteRepository.All()
                .Where(x => x.Card.CandidateId == candidateId && x.Card.VacancyId == vacancyId)
                .Select(x => x.ToSpecialNoteDto()).ToList();
        }
    }
}
