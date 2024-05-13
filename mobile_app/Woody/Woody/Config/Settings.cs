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

        /// <summary>
        /// Gets or sets the Connection string of the IoT Hub Device
        /// </summary>
        public string IOTHubDeviceConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the DeviceId of the IoT Hub Device
        /// </summary>
        public string IOTHubDeviceId { get; set; }

        /// <summary>
        /// Gets or sets the IoTHub Connection String
        /// </summary>
        public string IOTHubConnectionString { get; set; }

        public string BlobContainerName { get; set; }

        public string BlobConnectionString { get; set; }
    }
}
