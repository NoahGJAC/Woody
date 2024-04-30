using Firebase.Auth;
using Woody.Services;

namespace Woody.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void Btn_Login_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if (accessType != NetworkAccess.Internet)
        {
            await DisplayAlert("Error", "No internet", "OK");
        }

        try
        {

            var user = await AuthService.Client.SignInWithEmailAndPasswordAsync(user_name.Text, password.Text);

            AuthService.UserCreds = user;

            //Update UI
            lblUser.Text = $"ID    : {user_name.Text}\n";
            LoginView.IsVisible = false;
            LogoutView.IsVisible = true;

            lblError.Text = "Wrong Username or Password";
            await DisplayAlert("Success", "Successfully logged in", "OK");
            await Shell.Current.GoToAsync($"//Index");



        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        catch (ApplicationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error",ex.Message, "OK");
        }

    }

    private async void Btn_Logout_Clicked(object sender, EventArgs e)
    {
        try
        {
            AuthService.Client.SignOut();
            //Update UI
            lblUser.Text = string.Empty;
            LogoutView.IsVisible = false;
            LoginView.IsVisible = true;
            await Shell.Current.GoToAsync($"//Login");

        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
        }
    }

    private async void Btn_SignUp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}