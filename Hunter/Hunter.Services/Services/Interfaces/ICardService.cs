using Hunter.Services.Dto.ApiResults;
using System.Collections.Generic;

namespace Hunter.Services.Interfaces
{
    public interface ICardService
    {
        void AddCards(IEnumerable<CardDto> dto, string name);
    }
}
