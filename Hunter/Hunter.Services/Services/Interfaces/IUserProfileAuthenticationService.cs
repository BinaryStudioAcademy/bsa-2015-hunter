using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Services.Interfaces
{
    public interface IUserProfileAuthenticationService
    {
        int GetUserIdByUserLogin(string userLogin);
        IEnumerable<string> GetAuthUserIdByRole(string roleName, string userName);
    }
}
