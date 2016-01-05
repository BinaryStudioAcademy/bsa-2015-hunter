using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;

namespace Hunter.Services.Interfaces
{
    public interface IActivityHelperService
    {
        Task CreateAddedUserProfileActivity(UserProfile userProfile);
        Task CreateAddedCandidateActivity(Candidate candidate);
        Task CreateAddedVacancyActivity(Vacancy vacancy);
        Task CreateAddedPoolActivity(Pool pool);
        Task CreateUpdatedFeedbackActivity(Feedback feedback);
        Task CreateAddedSpecialNoteActivity(SpecialNote specialNote);
        Task CreateUpdatedSpecialNoteActivity(SpecialNote specialNote);
        Task CreateUploadedTestActivity(Card card);
        Task CreateUploadedResumeActivity(Candidate candidate);
        Task CreateUploadedPhotoActivity(Candidate candidate);
        Task CreateChangedCardStageActivity(Card card, Stage oldStage);
        Task CreateUpdateCandidateResolution(Candidate candidate, Resolution oldResolution);
    }
}
