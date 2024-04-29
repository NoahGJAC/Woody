namespace Woody.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();
		FrameSecurity.BindingContext = App.SecurityRepo;
		// get app resource colors
	}
}