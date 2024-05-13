using Firebase.Auth;
using System.Text.RegularExpressions;
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
    
    private Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
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
            //var user_ = new Models.User
            //{
            //    Uid = user.User.Uid,
            //    Username = user_name.Text
            //};
            //App.UserRepo.User = user_;
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

    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        LoginView.IsVisible = false;
        LogoutView.IsVisible = false;
        ForgotPasswordView.IsVisible = true;
    }

    private async void Btn_ResetPassword_Clicked(object sender, EventArgs e)
    {
        var email = Entry_email_reset.Text;
        if (email == null || !_emailRegex.Match(email).Success)
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }
        await AuthService.Client.ResetEmailPasswordAsync(email);
        await DisplayAlert("Password Reset", $"If this email address matches our records, you will receive a password reset email.", "OK");
        
        LoginView.IsVisible = true;
        ForgotPasswordView.IsVisible = false;
    }
}