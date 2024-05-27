using System.Text.RegularExpressions;
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

    private Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    public LoginPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoginView.IsVisible = true;
        lblError.Text = string.Empty;
        user_name.Text = string.Empty;
        password.Text = string.Empty;
    }

    private async void Btn_GoBack_Clicked(object sender, EventArgs e) {
        LoginView.IsVisible = true;
        LogoutView.IsVisible = false;
        ForgotPasswordView.IsVisible = false;
    }

    private async void Btn_Login_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if (accessType != NetworkAccess.Internet)
        {
            await DisplayAlert("Error", "No internet connection available.", "OK");
            return;
        }

        try
        {
            var user = await AuthService.Client.SignInWithEmailAndPasswordAsync(
                user_name.Text,
                password.Text
            );

            AuthService.UserCreds = user;

            //Update UI
            lblUser.Text = $"ID    : {user_name.Text}\n";
            LoginView.IsVisible = false;

            App.UserRepo.User = App
                .UserRepo.UserDb.Items.Where(u => u.Uid == user.User.Uid)
                .First();

            // Now that the user is authenticated, set the BindingContext for specific views
            var appShell = Shell.Current as AppShell;
            appShell.SetNavigationBindingContext();

            lblError.Text = "Wrong Username or Password";

            await DisplayAlert("Success", "Successfully logged in", "OK");
            await Shell.Current.GoToAsync($"//Index");
        }
        catch (FirebaseAuthException ex)
        {
            switch (ex.Reason)
            {

                case AuthErrorReason.AlreadyLinked:
                    await DisplayAlert("Error", "Account has already been linked.", "OK");
                    break;
                case AuthErrorReason.InvalidEmailAddress:
                    await DisplayAlert("Error", "Email address is invalid.", "OK");
                    break;
                case AuthErrorReason.MissingPassword:
                    await DisplayAlert("Error", "Provide a password.", "OK");
                    break;
                case AuthErrorReason.MissingEmail:
                    await DisplayAlert("Error", "Provide an email address.", "OK");
                    break;
                case AuthErrorReason.TooManyAttemptsTryLater:
                    await DisplayAlert("Error", "Too many attempts try later.", "OK");
                    break;
                case AuthErrorReason.UserNotFound:
                    await DisplayAlert("Error", "Account not found.", "OK");
                    break;
                case AuthErrorReason.UnknownEmailAddress:
                    await DisplayAlert("Error", "No records match with this email address.", "OK");
                    break;
                case AuthErrorReason.UserDisabled:
                    await DisplayAlert("Error", "Account has been disabled.", "OK");
                    break;
                case AuthErrorReason.WrongPassword:
                    await DisplayAlert("Error", "Wrong password.", "OK");
                    break;
                case AuthErrorReason.LoginCredentialsTooOld:
                    await DisplayAlert("Error", "Login credentials too old. Try again.", "OK");
                    break;
                case AuthErrorReason.Unknown:
                default:
                    if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
                    {
                        await DisplayAlert("Error", "Invalid login credentials.", "OK");
                        break;
                    }
                    await DisplayAlert("Error", $"An unknown error occurred. {ex.Message}", "OK");
                    break;
            }
        }
        catch (ApplicationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
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

        try 
        { 
            await AuthService.Client.ResetEmailPasswordAsync(email);
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.Reason == AuthErrorReason.ResetPasswordExceedLimit)
            {
                await DisplayAlert("Error", "You have exceeded the limit for password resets. Please try again later.", "OK");
                return;
            }
            else
            {
                await DisplayAlert("Error", "An error occurred while trying to reset your password. Please try again later.", "OK");
                return;
            }
        }
        await DisplayAlert("Password Reset", $"If this email address matches our records, you will receive a password reset email.", "OK");
        
        LoginView.IsVisible = true;
        LogoutView.IsVisible = true;
        ForgotPasswordView.IsVisible = false;
    }
}
