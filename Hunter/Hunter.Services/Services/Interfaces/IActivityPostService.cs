using System;
using System.Threading.Tasks;
using Hunter.DataAccess.Entities;

namespace Hunter.Services.Interfaces
{
    public interface IActivityPostService
    {
        Task Post(string message, ActivityType tag, Uri url = null);
    }
}