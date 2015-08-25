﻿using Hunter.Services.Dto.ApiResults;
using System.Collections.Generic;
using Hunter.Services.Dto;

namespace Hunter.Services.Interfaces
{
    public interface ICardService
    {
        void AddCards(IEnumerable<CardDto> dto, string name);
        bool UpdateCardStage(int vid, int cid, int stage);
        int GetCardStage(int vid, int cid);
        bool IsCardExist(int vid, int cid);
        void DeleteCard(int vid, int cid);
        IEnumerable<AppResultCardDto> GetApplicationResults(int vid, int cid);
    }
}