using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Services.Dto;
using Hunter.Services.Dto.ApiResults;

namespace Hunter.Services
{
    public interface IFeedbackService
    {
        IEnumerable<FeedbackHrInterviewDto> GetAllHrInterviews(int vid, int cid);
        IdApiResult SaveFeedback(FeedbackHrInterviewDto hrInterviewDto, string name);
    }
}
