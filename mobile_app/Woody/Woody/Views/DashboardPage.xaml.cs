namespace Woody.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();
		FrameSecurity.BindingContext = App.SecurityRepo;
		FrameSecurityDoor.BindingContext = App.SecurityRepo;
		FrameLocation.BindingContext = App.GeoLocationRepo;
	}
}