using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class CardExtension
    {
        public static CardDto ToCardDto(this Card card)
        {
            var dto = new CardDto
            {
                Id = card.Id,
                VacancyId = card.VacancyId,
                CandidateId = card.CandidateId,
                Added = card.Added,
                AddedByProfileId = card.AddedByProfileId,
                Stage = card.Stage
            };

            return dto;
        }

        public static void ToCardModel(this CardDto dto, Card card)
        {
            card.Id = dto.Id;
            card.VacancyId = dto.VacancyId;
            card.CandidateId = dto.CandidateId;
            card.Added = dto.Added;
            card.AddedByProfileId = dto.AddedByProfileId;
            card.Stage = dto.Stage;
        }
    }
}
