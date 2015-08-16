using Hunter.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;

namespace Hunter.Services.Extensions
{
    public static class FeedbackExtension
    {
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
