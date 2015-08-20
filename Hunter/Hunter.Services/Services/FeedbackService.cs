using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Dto.ApiResults;
using Hunter.DataAccess.Entities.Enums;
using Hunter.Services.Interfaces;

namespace Hunter.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger _logger;
        private readonly IActivityHelperService _activityHelperService;

        public FeedbackService(IFeedbackRepository feedbackRepository, ICardRepository cardRepository, ILogger logger, 
            IUserProfileRepository userProfileRepository, IActivityHelperService activityHelperService)
        {
            _feedbackRepository = feedbackRepository;
            _cardRepository = cardRepository;
            _logger = logger;
            _userProfileRepository = userProfileRepository;
            _activityHelperService = activityHelperService;
        }

        public IEnumerable<Dto.FeedbackDto> GetAllHrInterviews(int vid, int cid)
        {

            var card = _cardRepository.Query().SingleOrDefault(c => c.VacancyId == vid && c.CandidateId == cid);

            if (card == null)
                return null;
            try
            {
                var feedbacks = card.Feedback
                    .Where(f => (f.Type == 0 || f.Type == 1 || f.Type == 2))
                    .ToFeedbacksDto().ToList();

                if (!feedbacks.Any(f => f.Type == 0))
                {
                    feedbacks.Add(new FeedbackDto { Id = 0, Type = 0, CardId = card.Id, Text = "", Date = DateTime.Now, UserName = "" });
                }
                if (!feedbacks.Any(f => f.Type == 1))
                {
                    feedbacks.Add(new FeedbackDto { Id = 0, Type = 1, CardId = card.Id, Text = "", Date = DateTime.Now, UserName = "" });
                }
                if (!feedbacks.Any(f => f.Type == 2))
                {
                    feedbacks.Add(new FeedbackDto { Id = 0, Type = 2, CardId = card.Id, Text = "", Date = DateTime.Now, UserName = "" });
                }

                return feedbacks.OrderBy(f => f.Type);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }

        }

        public FeedbackDto GetTechInterview(int vacancyId, int candidateId)
        {
            int type = (int)FeedbackType.Expertise;
            try
            {
                var card = _cardRepository
                .Query()
                .Where(e => e.VacancyId == vacancyId && e.CandidateId == candidateId)
                .FirstOrDefault();

                var feedback = card.Feedback
                    .Where(e => e.Type == type)
                    .FirstOrDefault();
                if (feedback == null)
                    return new FeedbackDto() { Id = 0, Type = type, CardId = card.Id, Text = "", Date = DateTime.Now, UserName = "" };
                return feedback.ToFeedbackDto();
            }
            catch (Exception e)
            {
                _logger.Log(e);
                return null;
            }
             
        }

        public FeedbackDto GetSummary(int vacancyId, int candidateId)
        {
            try
            {
                var card = _cardRepository.Query()
                    .SingleOrDefault(e => e.VacancyId == vacancyId && e.CandidateId == candidateId);


                var summary = card.Feedback.FirstOrDefault(i => i.Type == (int) FeedbackType.Summary);
                if (summary == null)
                    return new FeedbackDto()
                    {
                        Id = 0,
                        Type = (int)FeedbackType.Summary,
                        CardId = card.Id,
                        Text = "",
                        Date = DateTime.Now,
                        UserName = ""
                    };

                return summary.ToFeedbackDto();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }
        }

        public FeedbackUpdatedResult SaveFeedback(FeedbackDto hrInterviewDto, string name)
        {
            Feedback feedback;
            var userProfile = _userProfileRepository.Get(u => u.UserLogin.ToLower() == name.ToLower());
            try
            {

                if (hrInterviewDto.Id != 0)
                {
                    feedback = _feedbackRepository.Get(hrInterviewDto.Id);
                    if (feedback == null) { 
    //                    return Api.NotFound(hrInterviewDto.Id);
                        throw new Exception("Feedback not found");
                    }
                }
                else
                {
                    feedback = new Feedback();
                }

                feedback.ProfileId = userProfile != null ? userProfile.Id : (int?)null;
                hrInterviewDto.ToFeedback(feedback);
            
            
                _feedbackRepository.UpdateAndCommit(feedback);
                _activityHelperService.CreateUpdatedFeedbackActivity(feedback);
                return new FeedbackUpdatedResult
                {
                    Id = feedback.Id,
                    Update = feedback.ToFeedbackDto().Date,
                    UserName = feedback.ToFeedbackDto().UserName
                };
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
//                return Api.Error((long)feedback.Id, ex.Message);
                throw ex;
            }
        }
    }
}
