using System.Reflection;
using Microsoft.Extensions.Configuration;
using Woody.Config;
using Woody.DataRepos;
using Woody.Services;
using Woody.Views;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody
{
    /// <summary>
    /// Represents the main application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the settings of the application.
        /// </summary>
        public static Settings Settings { get; private set; }
        private static SecurityRepo securityRepo;
        private static UserRepo userRepo;
        private static AzureIoTHubService ioTDevice;
        
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        public static UserRepo UserRepo
        {
            get { return userRepo ??= new UserRepo(); }
        }

        /// <summary>
        /// Gets the Security repository.
        /// </summary>
        public static SecurityRepo SecurityRepo
        {
            get { return securityRepo ??= new SecurityRepo(); }
        }

        private static PlantRepo plantRepo;

        /// <summary>
        /// Gets the plant repository.
        /// </summary>
        public static PlantRepo PlantRepo
        {
            get { return plantRepo ??= new PlantRepo(); }
        }

        private static GeoLocationRepo geoLocationRepo;

        /// <summary>
        /// Gets the geolocation repository
        /// </summary>
        public static GeoLocationRepo GeoLocationRepo
        {
            get { return geoLocationRepo ??= new GeoLocationRepo(); }
        }

        public static AzureIoTHubService IoTDevice
        {

            get { return ioTDevice ??= new AzureIoTHubService(); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            var a = Assembly.GetExecutingAssembly();
            var stream = a.GetManifestResourceStream("Woody.appsettings.json");

            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            Settings = config.GetRequiredSection(nameof(Settings)).Get<Settings>();
            MainPage = new AppShell();
            Task.Run(()=>IoTDevice.ConnectToDeviceAsync()).Wait();
        }
    }
}
