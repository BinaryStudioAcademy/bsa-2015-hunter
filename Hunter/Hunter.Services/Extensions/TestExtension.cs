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
            return new TestDto
            {
                Id = test.Id,
                CardId = test.CardId,
                Comment = test.Comment,
                FeedbackId = test.FeedbackId,
                FileId = test.FileId,
                Url = test.Url,
                File = test.File.ToFileDto()
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
            testDto.File.ToFile(test.File = new File());
        }
    }
}
