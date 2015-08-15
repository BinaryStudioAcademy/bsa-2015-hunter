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
        private readonly ICardRepository _cardRepository;
        //private readonly IUserProfileRepository _userProfileRepository;

        public SpecialNoteService(ISpecialNoteRepository specialNoteRepository, ICardRepository cardRepository)
        {
            _specialNoteRepository = specialNoteRepository;
            _cardRepository = cardRepository;
            //_userProfileRepository = userProfileRepository;
        }

        public IEnumerable<SpecialNoteDto> GetAllSpecialNotes()
        {
            return _specialNoteRepository.All().Select(x => x.ToSpecialNoteDto());
        }


        public SpecialNoteDto GetSpecialNoteById(int id)
        {
            return _specialNoteRepository.Get(id).ToSpecialNoteDto();
        }


        public void AddSpecialNote(SpecialNoteDto entity, int vid, int cid)
        {
            var cardId = _cardRepository.Query().FirstOrDefault(x => x.CandidateId == cid && x.VacancyId == vid).Id;
            entity.CardId = cardId;
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

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForUser(string login, int vacancyId, int candidateId)
        {
            return
                _specialNoteRepository.Query()
                    .Where(x => x.UserLogin == login && x.Card.CandidateId == candidateId && x.Card.VacancyId == vacancyId )
                    .OrderByDescending(x => x.LastEdited)
                    .ToList()
                    .Select(x => x.ToSpecialNoteDto());
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCandidate(int candidateId)
        {
            return _specialNoteRepository.Query().Where(x => x.Card.CandidateId == candidateId)
                .OrderByDescending(x => x.LastEdited)
                .ToList()
                .Select(x => x.ToSpecialNoteDto());
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCard(int vacancyId, int candidateId)
        {
            return _specialNoteRepository.Query()
                .Where(x => x.Card.CandidateId == candidateId && x.Card.VacancyId == vacancyId)
                .OrderByDescending(x => x.LastEdited)
                .ToList()
                .Select(x => x.ToSpecialNoteDto());
        }
    }
}
