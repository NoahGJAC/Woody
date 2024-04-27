namespace Woody.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Btn_Login_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Index");
    }
}