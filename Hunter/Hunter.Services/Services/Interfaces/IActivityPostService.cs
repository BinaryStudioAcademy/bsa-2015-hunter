using System;

namespace Hunter.Services.Interfaces
{
    // TODO Replace with enum
    public static class ActivityTypes
    {
        public const string UserAdded = "new user added";
        public const string UserDeleted = "new user deleted";
        public const string CandidateAdded = "New candidate added";
    }

    public interface IActivityPostService
    {
        void Post(string message, string tag, Uri url = null);
    }
}