﻿using System.Collections.Generic;
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
            return _specialNoteRepository.Query().ToList().Select(x => x.ToDto());
        }


        public SpecialNoteDto GetSpecialNoteById(int id)
        {
            return _specialNoteRepository.Get(id).ToDto();
        }


        public void AddSpecialNote(SpecialNoteDto entity, int vid, int cid)
        {
            var card = _cardRepository.GetByCandidateAndVacancy(cid, vid);
            entity.CardId = card.Id;
            _specialNoteRepository.UpdateAndCommit(entity.ToEntity());
        }

        public void UpdateSpecialNote(SpecialNoteDto entity)
        {
            _specialNoteRepository.UpdateAndCommit(entity.ToEntity());
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
