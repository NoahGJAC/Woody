namespace Woody.Views;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

/// <summary>
/// Represents a dashboard page for the user.
/// </summary>
public partial class DashboardPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardPage"/> class.
    /// </summary>
    public DashboardPage()
	{
		InitializeComponent();
		FrameSecurity.BindingContext = App.FarmRepo.SecurityRepo;
		FrameSecurityDoor.BindingContext = App.FarmRepo.SecurityRepo;
		FrameLocation.BindingContext = App.FarmRepo.GeoLocationRepo;
		FramePlant.BindingContext = App.FarmRepo.PlantRepo;

	}
}