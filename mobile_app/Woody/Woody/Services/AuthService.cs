using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Services
{
    /// <summary>
    /// Provides authentication services using Firebase.
    /// </summary>
    public class AuthService
    {
        // Configure...
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        private static FirebaseAuthConfig config = new FirebaseAuthConfig
        {
            ApiKey = App.Settings.FireBaseApiKey,
            AuthDomain = App.Settings.FirebaseAuthorizedDomain,
            Providers = new FirebaseAuthProvider[]
            {
                // Add and configure individual providers
                new EmailProvider()
            },
        };
        // ...and create your FirebaseAuthClient

        /// <summary>
        /// Gets the Firebase authentication client configured with the specified settings.
        /// </summary>
        public static FirebaseAuthClient Client { get; } = new FirebaseAuthClient(config);

        /// <summary>
        /// Gets or sets the user credentials obtained from Firebase authentication.
        /// </summary>
        public static UserCredential UserCreds { get; set; }
    }
}
