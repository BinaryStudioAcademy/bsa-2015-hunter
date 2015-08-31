using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Extensions
{
    public static class TestForCheckExtension
    {
        public static TestForCheckDto ToTestForCheckDto(this Test test)
        {
            var feedbackDto = test.FeedbackId != null ? test.Feedback.ToFeedbackDto() : null;
            FileDto file = test.File != null ? test.File.ToFileDto() : null;

            return new TestForCheckDto
            {
                Id = test.Id,
                CardId = test.CardId,
                Comment = test.Comment,
                FeedbackId = test.FeedbackId,
                FileId = test.FileId,
                UserProfileId = test.UserProfileId,
                Added = test.Added,
                Url = test.Url,
                File = file,
                Feedback = feedbackDto,
                IsChecked = test.IsChecked,
                VacancyId = test.Card.VacancyId,
                CandidateId = test.Card.CandidateId
            };
        }

        public static void ToTestForCheck(this TestForCheckDto testDto, Test test)
        {
            test.Id = testDto.Id;
            test.CardId = testDto.CardId;
            test.Comment = testDto.Comment;
            test.FeedbackId = testDto.FeedbackId;
            test.FileId = testDto.FileId;
            test.Url = testDto.Url;
            test.Added = testDto.Added;
            test.UserProfileId = testDto.UserProfileId;
            test.IsChecked = test.IsChecked;

            if (test.File != null)
                testDto.File.ToFile(test.File = new File());

            if (testDto.Feedback != null)
                testDto.Feedback.ToFeedback((test.Feedback = new Feedback()));
        }
    }
}
