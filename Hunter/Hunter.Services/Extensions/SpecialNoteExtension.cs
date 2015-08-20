using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    static class SpecialNoteExtension
    {
        public static SpecialNoteDto ToDto(this SpecialNote specialNote)
        {
            var specialNotesDto = new SpecialNoteDto
            {
                Id = specialNote.Id,
                UserLogin = specialNote.UserProfile.UserLogin,
                Text = specialNote.Text,
                LastEdited = specialNote.LastEdited,
                CardId = specialNote.CardId,
                UserAlias = specialNote.UserProfile.Alias
            };

            return specialNotesDto;
        }

        public static SpecialNote ToEntity(this SpecialNoteDto specialNotesDto)
        {
            var specialNote = new SpecialNote()
            {
                Id = specialNotesDto.Id,
                Text = specialNotesDto.Text,
                LastEdited = specialNotesDto.LastEdited,
                CardId = specialNotesDto.CardId
            };

            return specialNote;
        }
    }
}
