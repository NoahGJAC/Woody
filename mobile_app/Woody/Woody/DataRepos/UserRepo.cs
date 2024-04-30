using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Models;
using Woody.Services;

namespace Woody.DataRepos
{

    /// <summary>
    /// Represents a repository for user data, providing access to the user database service.
    /// </summary>
    public class UserRepo
    {
        private UserDatabaseService<User> userDb;

        /// <summary>
        /// Gets the user database service.
        /// </summary>
        /// <value>
        /// An instance of <see cref="UserDatabaseService{User}"/> that provides access to the user database.
        /// </value>
        public UserDatabaseService<User> UserDb
        {
            get
            {
                return userDb ??= new UserDatabaseService<User>(AuthService.UserCreds.User, nameof(User), App.Settings.FirebaseDatabaseUrl);
            }
        }
    }
}
