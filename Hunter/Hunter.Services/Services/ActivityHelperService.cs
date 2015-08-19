using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites.Enums;
using Hunter.DataAccess.Entities.Enums;
using Hunter.DataAccess.Interface;
using Hunter.Services.Interfaces;

namespace Hunter.Services.Services
{
    public class ActivityHelperService : IActivityHelperService
    {
        private readonly IActivityPostService _activityPostService;
        
        public ActivityHelperService(IActivityPostService activityPostService)
        {
            _activityPostService = activityPostService;
        }

        public void CreateAddedUserProfileActivity(UserProfile userProfile)
        {
            string message = string.Format("{0} has joined Hunter", userProfile.UserLogin);
            ActivityType type = ActivityType.User;
            Uri url = new Uri("#/user/edit/" + userProfile.Id, UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateAddedCandidateActivity(Candidate candidate)
        {
            string message = string.Format("A new candidate has been added : {0} {1}", candidate.FirstName,
                candidate.LastName);
            ActivityType type = ActivityType.Candidate;
            Uri url = new Uri("#/candidate/"+candidate.Id, UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateAddedVacancyActivity(Vacancy vacancy)
        {
            string message = string.Format("A new vacancy has been created : {0}", vacancy.Name);
            ActivityType type = ActivityType.Vacancy;
            Uri url = new Uri("#/vacancy/" + vacancy.Id, UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateAddedPoolActivity(Pool pool)
        {
            string message = string.Format("A new pool has been created : {0}", pool.Name);
            ActivityType type = ActivityType.Pool;
            Uri url = new Uri("#/pool", UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateUpdatedFeedbackActivity(Feedback feedback)
        {         
            string feedbackType;
            string feedbackRoute;
            
            switch (feedback.Type)
            {
                case 0:
                    feedbackType = "English";
                    feedbackRoute = "hrinterview";
                    break;
                case 1:
                    feedbackType = "Personal";
                    feedbackRoute = "hrinterview";
                    break;
                case 2:
                    feedbackType = "Expertise";
                    feedbackRoute = "technicalinterview";
                    break;
                case 3:
                    feedbackType = "Test";
                    feedbackRoute = "test";
                    break;
                default:
                    feedbackType = "";
                    feedbackRoute = "";
                    break;
            }

            string message = string.Format("{0} feedback for {1} {2} on {3} has been updated", 
                feedbackType, feedback.Card.Candidate.FirstName, feedback.Card.Candidate.LastName, feedback.Card.Vacancy.Name);
            ActivityType type = ActivityType.Feedback;
            //todo: change activity url to /feedback.Card/:id when the latter is implemented
            Uri url = new Uri(string.Format("#/vacancy/{0}/candidate/{1}?tab={2}",
                feedback.Card.VacancyId, feedback.Card.CandidateId, feedbackRoute), UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateAddedSpecialNoteActivity(SpecialNote specialNote)
        {
            string message = string.Format("A special note for {0} {1} on {2} has been added",
                specialNote.Card.Candidate.FirstName, specialNote.Card.Candidate.LastName, specialNote.Card.Vacancy.Name);
            ActivityType type = ActivityType.SpecialNote;
            Uri url = new Uri(string.Format("#/vacancy/{0}/candidate/{1}?tab={2}",
                specialNote.Card.VacancyId, specialNote.Card.CandidateId, "specialnotes"), UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateUpdatedSpecialNoteActivity(SpecialNote specialNote)
        {
            string message = string.Format("A special note for {0} {1} on {2} has been updated",
            specialNote.Card.Candidate.FirstName, specialNote.Card.Candidate.LastName, specialNote.Card.Vacancy.Name);
            ActivityType type = ActivityType.SpecialNote;
            Uri url = new Uri(string.Format("#/vacancy/{0}/candidate/{1}?tab={2}",
                specialNote.Card.VacancyId, specialNote.Card.CandidateId, "specialnotes"), UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateUploadedResumeActivity(Candidate candidate)
        {
            string message = string.Format("A resume of {0} {1} has been uploaded ", candidate.FirstName,
                candidate.LastName);
            ActivityType type = ActivityType.Resume;
            Uri url = new Uri("#/candidate/" + candidate.Id, UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateUploadedTestActivity(Card card)
        {
            string message = string.Format("A test of {0} {1} on {2} has been uploaded", card.Candidate.FirstName,
                card.Candidate.LastName, card.Vacancy.Name);
            ActivityType type = ActivityType.Test;
            Uri url = new Uri(string.Format("#/vacancy/{0}/candidate/{1}?tab={2}",
                card.VacancyId, card.CandidateId, "test"), UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }

        public void CreateUploadedPhotoActivity(Candidate candidate)
        {
            string message = string.Format("A photo of {0} {1} has been uploaded ", candidate.FirstName,
    candidate.LastName);
            ActivityType type = ActivityType.Photo;
            Uri url = new Uri("#/candidate/" + candidate.Id, UriKind.Relative);
            _activityPostService.Post(message, type, url);
        }
    }
}
