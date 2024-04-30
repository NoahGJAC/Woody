using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Config
{
    /// <summary>
    /// Represents the configuration settings for the app.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets or sets the authorized domain for Firebase.
        /// </summary>
        public string FirebaseAuthorizedDomain { get; set; }

        /// <summary>
        /// Gets or sets the API key for Firebase.
        /// </summary>
        public string FireBaseApiKey { get; set; }

        /// <summary>
        /// Gets or sets the URL for the Firebase database.
        /// </summary>
        public string FirebaseDatabaseUrl { get; set; }
    }
}
