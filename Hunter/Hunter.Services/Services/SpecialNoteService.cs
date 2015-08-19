using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class SpecialNoteService : ISpecialNoteService
    {
        private readonly ISpecialNoteRepository _specialNoteRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IActivityHelperService _activityHelperService;
        //private readonly IUserProfileRepository _userProfileRepository;

        public SpecialNoteService(ISpecialNoteRepository specialNoteRepository, ICardRepository cardRepository,
            IActivityHelperService activityHelperService)
        {
            _specialNoteRepository = specialNoteRepository;
            _cardRepository = cardRepository;
            _activityHelperService = activityHelperService;
            //_userProfileRepository = userProfileRepository;
        }

        public IEnumerable<SpecialNoteDto> GetAllSpecialNotes()
        {
            return _specialNoteRepository.Query().ToList().Select(x => x.ToDto());
        }


        public SpecialNoteDto GetSpecialNoteById(int id)
        {
            return _specialNoteRepository.Get(id).ToDto();
        }


        public void AddSpecialNote(SpecialNoteDto dto, int vid, int cid)
        {
            var card = _cardRepository.GetByCandidateAndVacancy(cid, vid);
            dto.CardId = card.Id;
            var specialNote = dto.ToEntity();
            _specialNoteRepository.UpdateAndCommit(specialNote);
            _activityHelperService.CreateAddedSpecialNoteActivity(specialNote);
        }

        public void UpdateSpecialNote(SpecialNoteDto dto)
        {
            var specialNote = dto.ToEntity();
            _specialNoteRepository.UpdateAndCommit(specialNote);
            _activityHelperService.CreateUpdatedSpecialNoteActivity(specialNote);
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
                    .Select(x => x.ToDto());
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCandidate(int candidateId)
        {
            return _specialNoteRepository.Query().Where(x => x.Card.CandidateId == candidateId)
                .OrderByDescending(x => x.LastEdited)
                .ToList()
                .Select(x => x.ToDto());
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForCard(int vacancyId, int candidateId)
        {
            return _specialNoteRepository.Query()
                .Where(x => x.Card.CandidateId == candidateId && x.Card.VacancyId == vacancyId)
                .OrderByDescending(x => x.LastEdited)
                .ToList()
                .Select(x => x.ToDto());
        }
    }
}
