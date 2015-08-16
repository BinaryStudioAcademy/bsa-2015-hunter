using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.Services.Dto;

namespace Hunter.Services.Extensions
{
    public static class TestExtension
    {
        public static TestDto ToTestDto(this Test test)
        {
            var feedbackDto = test.FeedbackId != null ? test.Feedback.ToFeedbackDto() : null;

            return new TestDto
            {
                Id = test.Id,
                CardId = test.CardId,
                Comment = test.Comment,
                FeedbackId = test.FeedbackId,
                FileId = test.FileId,
                Url = test.Url,
                File = test.File.ToFileDto(),
                Feedback = feedbackDto
            };
        }

        public static void ToTest(this TestDto testDto, Test test)
        {
            test.Id = testDto.Id;
            test.CardId = testDto.CardId;
            test.Comment = testDto.Comment;
            test.FeedbackId = testDto.FeedbackId;
            test.FileId = testDto.FileId;
            test.Url = testDto.Url;

            if(test.FileId != null)
                testDto.File.ToFile(test.File = new File());

            if (test.FeedbackId != null)
                testDto.Feedback.ToFeedback((test.Feedback = new Feedback()));
        }
    }
}
