using Woody.Services;

namespace Woody
{
    /*
     * Team: Woody
     * Section 1
     * Winter 2024, 04/30/2024
     * 420-6A6 App Dev III
     */

    /// <summary>
    /// Represents the main shell of the application.
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppShell"/> class.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();
        }

        public void SetNavigationBindingContext()
        {
            if(App.UserRepo.User != null)
            {
                tab_analytics.BindingContext = App.UserRepo.User.UserType;
                tab_geolocation.BindingContext = App.UserRepo.User.UserType;
            }
        }
    }
}
