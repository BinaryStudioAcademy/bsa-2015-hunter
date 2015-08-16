﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.Services.Dto;
using Hunter.Services.Extensions;
using Hunter.Services.Dto.ApiResults;

namespace Hunter.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger _logger;

        public FeedbackService(IFeedbackRepository feedbackRepository, ICardRepository cardRepository, ILogger logger, IUserProfileRepository userProfileRepository)
        {
            _feedbackRepository = feedbackRepository;
            _cardRepository = cardRepository;
            _logger = logger;
            _userProfileRepository = userProfileRepository;
        }

        public IEnumerable<Dto.FeedbackHrInterviewDto> GetAllHrInterviews(int vid, int cid)
        {

            var card = _cardRepository.Query().SingleOrDefault(c => c.VacancyId == vid && c.CandidateId == cid);

            if (card == null)
                return null;
            try
            {
                var feedbacks = card.Feedback
                    .Where(f => (f.Type == 0 || f.Type == 1 || f.Type == 2))
                    .ToFeedbackHrInterviewDto().ToList();

                if (!feedbacks.Any(f => f.Type == 0))
                {
                    feedbacks.Add(new FeedbackHrInterviewDto { Id = 0, Type = 0, CardId = card.Id, Text = "", Date = "", UserName = "" });
                }
                if (!feedbacks.Any(f => f.Type == 1))
                {
                    feedbacks.Add(new FeedbackHrInterviewDto { Id = 0, Type = 1, CardId = card.Id, Text = "", Date = "", UserName = "" });
                }
                if (!feedbacks.Any(f => f.Type == 2))
                {
                    feedbacks.Add(new FeedbackHrInterviewDto { Id = 0, Type = 2, CardId = card.Id, Text = "", Date = "", UserName = "" });
                }

                return feedbacks.OrderBy(f => f.Type);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return null;
            }

        }

        public IdApiResult SaveFeedback(FeedbackHrInterviewDto hrInterviewDto, string name)
        {
            Feedback feedback;
            var userProfile = _userProfileRepository.Get(u => u.UserLogin.ToLower() == name.ToLower());
            
            if (hrInterviewDto.Id != 0)
            {
                feedback = _feedbackRepository.Get(hrInterviewDto.Id);
                if (feedback == null)
                    return Api.NotFound(hrInterviewDto.Id);
            }
            else
            {
                feedback = new Feedback();
            }

            feedback.ProfileId = userProfile != null ? userProfile.Id : (int?)null;
            hrInterviewDto.ToFeedback(feedback);
            
            try
            {
                _feedbackRepository.UpdateAndCommit(feedback);
                return Api.Updated(feedback.Id);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Api.Error((long)feedback.Id, ex.Message);
            }
        }
    }
}