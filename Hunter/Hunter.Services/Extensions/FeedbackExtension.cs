﻿using System;
﻿using Hunter.Services.Dto;
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
//        public static FeedbackDto ToFeedbackDto(this Feedback feedback)
//        {
//            return new FeedbackDto
//            {
//                Added = feedback.Added,
//                CardId = feedback.CardId,
//                Edited = feedback.Edited,
//                Id = feedback.Id,
//                ProfileId = feedback.ProfileId,
//                Status = feedback.Status,
//                Text = feedback.Text,
//                Type = feedback.Type
//            };
//        }
//
//        public static void ToFeedback(this FeedbackDto feedbackDto, Feedback feedback)
//        {
//            feedback.Id = feedbackDto.Id;
//            feedback.CardId = feedbackDto.CardId;
//            feedback.Edited = feedbackDto.Edited;
//            feedback.ProfileId = feedbackDto.ProfileId;
//            feedback.Status = feedbackDto.Status;
//            feedback.Text = feedbackDto.Text;
//            feedback.Type = feedbackDto.Type;
//        }

        public static IEnumerable<FeedbackHrInterviewDto> ToFeedbackHrInterviewDto(this IEnumerable<Feedback> feedbacks)
        {
            return feedbacks.Select(f => new FeedbackHrInterviewDto()
            {
                Id = f.Id,
                CardId = f.CardId,
                Type = f.Type,
                Text = f.Text,
                Date = f.Edited == null ? f.Added.ToString("dd.MM.yy") : f.Edited.Value.ToString("dd.MM.yy"),
                UserName = f.UserProfile != null
                    ? f.UserProfile.UserLogin.Substring(0, f.UserProfile.UserLogin.IndexOf("@"))
                    : ""
            }).OrderBy(f => f.Type);
        }

        public static FeedbackHrInterviewDto ToFeedbackHrInterviewDto(this Feedback feedback)
        {
            return new FeedbackHrInterviewDto
            {
                Id = feedback.Id,
                CardId = feedback.CardId,
                Type = feedback.Type,
                Text = feedback.Text,
                Date =
                    feedback.Edited == null
                        ? feedback.Added.ToString("dd.MM.yy")
                        : feedback.Edited.Value.ToString("dd.MM.yy"),
                UserName = feedback.UserProfile != null
                    ? feedback.UserProfile.UserLogin.Substring(0, feedback.UserProfile.UserLogin.IndexOf("@"))
                    : ""
            };
        }

        public static void ToFeedback(this FeedbackHrInterviewDto hrInterviewDto, Feedback feedback)
        {
            feedback.CardId = hrInterviewDto.CardId;
            feedback.Type = hrInterviewDto.Type;
            feedback.Text = hrInterviewDto.Text;
            if (hrInterviewDto.Id != 0)
            {
                feedback.Edited = DateTime.Now;
            }
            else
            {
                feedback.Added = DateTime.Now;
            }
        }
    }
}
