﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;

namespace Hunter.Services.Services.Interfaces
{
    public interface ISpecialNoteService
    {
        IEnumerable<SpecialNoteDto> GetAllSpecialNotes();

        SpecialNoteDto GetSpecialNoteById(int id);

        void AddSpecialNote(SpecialNoteDto entity);

        void UpdateSpecialNote(SpecialNoteDto entity);

        void DeleteSpecialNoteById(int id);

        IEnumerable<SpecialNoteDto> GetSpecialNotesForUser(string login);

        IEnumerable<SpecialNoteDto> GetSpecialNotesForCandidate(int candidateId);

        IEnumerable<SpecialNoteDto> GetSpecialNotesForCard(int vacancyId, int candidateId);
    }
}
