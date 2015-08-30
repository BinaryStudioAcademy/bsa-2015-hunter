using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services.Extensions;
using Hunter.Services.Interfaces;
using Hunter.DataAccess.Entities;

namespace Hunter.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICardRepository _cardRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger _logger;
        private readonly IActivityHelperService _activityHelperService;

        public CardService(ICardRepository cardRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork, ILogger logger, IActivityHelperService activityHelperService)
        {
            _cardRepository = cardRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _activityHelperService = activityHelperService;
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

        public bool UpdateCardStage(int vid, int cid, int stage)
        {
            var card = _cardRepository.Get(c => c.VacancyId == vid && c.CandidateId == cid);
            if (card == null) return false;
            Stage oldStage = (Stage) card.Stage;
            card.Stage = stage;
            _cardRepository.UpdateAndCommit(card);
            _activityHelperService.CreateChangedCardStageActivity(card, oldStage);
            return true;
        }

        public int GetCardStage(int vid, int cid)
        {
            var card = _cardRepository.Get(c => c.VacancyId == vid && c.CandidateId == cid);
            if (card == null) return 0;
            return card.Stage;
        }

        public bool IsCardExist(int vid, int cid)
        {
            try
            {
                return _cardRepository.Query().Any(c => c.VacancyId == vid && c.CandidateId == cid);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return false;
            }
        }

        public void DeleteCard(int vid, int cid)
        {
            try
            {
                _cardRepository.DeleteAndCommit(_cardRepository.Get(c => c.VacancyId == vid && c.CandidateId == cid));

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }

        public IEnumerable<AppResultCardDto> GetApplicationResults(int cid)
        {
            try
            {
                var appResults = _cardRepository.Query()
                    .Where(c => c.CandidateId == cid)
                    .Select(c => new AppResultCardDto()
                {
                        CardId = c.Id,
                    VacancyId = c.VacancyId,
                    Stage = c.Stage,
                    Date = c.Added,
                            Vacancy = c.Vacancy.Name
                        })
                    .OrderByDescending(c => c.Date);

                return appResults;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new List<AppResultCardDto>();
            }
        }

        public AppResultCardDto GetCardInfo(int vid, int cid)
        {
            try
            {
                var appResults = _cardRepository.Query()
                    .Where(c => c.VacancyId == vid && c.CandidateId == cid)
                    .Select(c => new AppResultCardDto()
                    {
                        CardId = c.Id,
                        VacancyId = c.VacancyId,
                        Stage = c.Stage,
                        Date = c.Added,
                    }).FirstOrDefault();

                return appResults;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw new Exception("no card found!");
            }
        }
    }
}
