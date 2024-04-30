namespace Woody.Views;

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
		FrameSecurity.BindingContext = App.SecurityRepo;
		FrameSecurityDoor.BindingContext = App.SecurityRepo;
		FrameLocation.BindingContext = App.GeoLocationRepo;
	}
}