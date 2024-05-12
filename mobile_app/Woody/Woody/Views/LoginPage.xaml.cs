using Firebase.Auth;
using Woody.Services;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents a login page for the user.
/// </summary>
public partial class LoginPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPage"/> class.
    /// </summary>
	public LoginPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoginView.IsVisible = true;
        LogoutView.IsVisible = false;
        lblError.Text = string.Empty;
        user_name.Text = string.Empty;
        password.Text = string.Empty;
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