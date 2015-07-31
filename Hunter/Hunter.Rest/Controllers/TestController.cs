using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hunter.DataAccess.Db;
using Hunter.Services;

namespace Hunter.Rest.Controllers
{
    public class TestController : ApiController
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }


        public IEnumerable<User> GetUsers()
        {
            var users = _userService.GetAllUsers();
            List<User> uss = new List<User>();
            foreach (var user in users)
            {
                    uss.Add(new User()
                    {
                        Login = user.Login,

                    });
            }

            return uss;
        } 

    }
}
