using Woody.DataRepos;
using Woody.Views;

namespace Woody
{
    public partial class App : Application
    {
        private static SecurityRepo securityRepo;
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

            MainPage = new AppShell();
        }
    }
}
