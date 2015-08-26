﻿using System.Collections.Generic;
using System.Linq;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Dto.ApiResults;
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
        private readonly IUserProfileRepository _userProfileRepository;

        public SpecialNoteService(ISpecialNoteRepository specialNoteRepository, ICardRepository cardRepository,
            IActivityHelperService activityHelperService, IUserProfileRepository userProfileRepository)
        {
            _specialNoteRepository = specialNoteRepository;
            _cardRepository = cardRepository;
            _activityHelperService = activityHelperService;
            _userProfileRepository = userProfileRepository;
        }

        public IEnumerable<SpecialNoteDto> GetAllSpecialNotes()
        {
            return _specialNoteRepository.Query().ToList().Select(x => x.ToDto());
        }


        public SpecialNoteDto GetSpecialNoteById(int id)
        {
            return _specialNoteRepository.Get(id).ToDto();
        }

        public SpecNoteResult AddSpecialNote(SpecialNoteDto dto, int vid, int cid)
        {
            var card = _cardRepository.GetByCandidateAndVacancy(cid, vid);
            dto.CardId = card.Id;
            var specnoteEntity = dto.ToEntity();
            var userProfile = _userProfileRepository.Get(x => x.UserLogin == dto.UserLogin);
            specnoteEntity.UserProfileId = userProfile != null ? userProfile.Id : (int?)null;
            _specialNoteRepository.UpdateAndCommit(specnoteEntity);
            _activityHelperService.CreateAddedSpecialNoteActivity(specnoteEntity);

            return new SpecNoteResult
            {
                Id = specnoteEntity.Id,
                UserAlias = specnoteEntity.ToDto().UserAlias,
                Update = specnoteEntity.ToDto().LastEdited,
                CardId = specnoteEntity.CardId
            };
        }

        public SpecNoteResult UpdateSpecialNote(SpecialNoteDto dto)
        {
            var specnoteEntity = dto.ToEntity();
            var userProfile = _userProfileRepository.Get(x => x.UserLogin == dto.UserLogin);
            specnoteEntity.UserProfileId = userProfile != null ? userProfile.Id : (int?)null;
            _specialNoteRepository.UpdateAndCommit(specnoteEntity);
            _activityHelperService.CreateUpdatedSpecialNoteActivity(specnoteEntity);

            return new SpecNoteResult
            {
                Id = specnoteEntity.Id,
                UserAlias = specnoteEntity.ToDto().UserAlias,
                Update = specnoteEntity.ToDto().LastEdited,
                CardId = specnoteEntity.CardId
            };
        }


        public void DeleteSpecialNoteById(int id)
        {
            var specialNote = _specialNoteRepository.Get(id);
            _specialNoteRepository.DeleteAndCommit(specialNote);
        }

        public IEnumerable<SpecialNoteDto> GetSpecialNotesForUser(string login, int candidateId)
        {
            return
                _specialNoteRepository.Query()
                    .Where(x => x.UserProfile.UserLogin == login && x.Card.CandidateId == candidateId )
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
