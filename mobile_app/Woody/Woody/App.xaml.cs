using System.Reflection;
using Woody.Config;
using Woody.DataRepos;
using Microsoft.Extensions.Configuration;
using Woody.Views;

namespace Woody
{
    public partial class App : Application
    {
        public static Settings Settings { get; private set; }
        private static SecurityRepo securityRepo;
        private static UserRepo userRepo;
        public static UserRepo UserRepo
        {
            get
            {
                return userRepo ??= new UserRepo();
            }
        }
        public static SecurityRepo SecurityRepo
        {
            get
            {
                return securityRepo ??= new SecurityRepo();
            }
        }
        public App()
        {
            InitializeComponent();
            var a = Assembly.GetExecutingAssembly();
            var stream = a.GetManifestResourceStream("Woody.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
            Settings = config.GetRequiredSection(nameof(Settings)).Get<Settings>();
            MainPage = new AppShell();
        }
    }
}
