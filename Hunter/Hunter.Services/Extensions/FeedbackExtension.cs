using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class FeedbackExtension
    {
        public static FeedbackDto ToFeedbackDto(this Feedback feedback)
        {
            return new FeedbackDto
            {
                Added = feedback.Added,
                CardId = feedback.CardId,
                Edited = feedback.Edited,
                Id = feedback.Id,
                ProfileId = feedback.ProfileId,
                Status = feedback.Status,
                Text = feedback.Text,
                Type = feedback.Type
            };
        }

        public static void ToFeedback(this FeedbackDto feedbackDto, Feedback feedback)
        {
            feedback.Id = feedbackDto.Id;
            feedback.CardId = feedbackDto.CardId;
            feedback.Edited = feedbackDto.Edited;
            feedback.ProfileId = feedbackDto.ProfileId;
            feedback.Status = feedbackDto.Status;
            feedback.Text = feedbackDto.Text;
            feedback.Type = feedbackDto.Type;
        }
    }
}
