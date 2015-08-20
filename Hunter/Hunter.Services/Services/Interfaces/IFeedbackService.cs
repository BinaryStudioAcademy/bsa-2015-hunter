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
        IEnumerable<FeedbackDto> GetAllHrInterviews(int vid, int cid);
        FeedbackUpdatedResult SaveFeedback(FeedbackDto hrInterviewDto, string name);
        FeedbackDto GetTechInterview(int vacancyId, int candidateId);
        FeedbackDto GetSummary(int vacancyId, int candidateId);
    }
}
