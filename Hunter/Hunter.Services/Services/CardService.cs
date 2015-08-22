using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Interfaces;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Dto.ApiResults;
using Hunter.DataAccess.Entities;

namespace Hunter.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICardRepository _cardRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger _logger;

        public CardService (ICardRepository cardRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _cardRepository = cardRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void AddCards(IEnumerable<CardDto> dtoCards, string name)
        {
            //if (dtoCards == null) return;

            //var newCard = new Card();
            
            var userProfile = _userProfileRepository.Get(u => u.UserLogin.ToLower() == name.ToLower());

            var userProfileId = userProfile != null ? userProfile.Id : (int?)null;

            foreach (var dto in dtoCards)
            {
                if (_cardRepository.Query().Any(c => (c.CandidateId == dto.CandidateId && c.VacancyId == dto.VacancyId)))
                {
                    continue;    
                }
                
                _cardRepository.UpdateAndCommit(dto.ToCardModel(userProfileId));
            }
        }
    }
}
