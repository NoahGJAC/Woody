using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Models;
using Woody.Services;

namespace Woody.DataRepos
{
    public class UserRepo
    {
        private UserDatabaseService<User> userDb;
        public UserDatabaseService<User> UserDb
        {
            get
            {
                return userDb ??= new UserDatabaseService<User>(AuthService.UserCreds.User, nameof(User), App.Settings.FirebaseDatabaseUrl);
            }
        }
    }
}
