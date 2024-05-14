
using Firebase.Auth;
using Woody.Enums;
using Woody.Services;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents a Sign in page for the user.
/// </summary>
public partial class SignUpPage : ContentPage
{
    /// <summary>
    /// Gets or sets The user model.
    /// </summary>
    public Models.User user_ { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpPage"/> class.
    /// </summary>
    /// <param name="item">The user model.</param>
	public SignUpPage(Models.User item = null)
	{
		InitializeComponent();
        if(item != null)
        {
            user_ = item;
        }
        else
        {
            user_ = new Models.User();
        }
        BindingContext = user_;

	}

    private async void Btn_SignUp_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if (accessType != NetworkAccess.Internet)
        {
            await DisplayAlert("Error", "No internet connection available.", "OK");
            return;
        }
        if (string.IsNullOrEmpty(user_name.Text))
        {
            await DisplayAlert("Error", "Please enter a username", "OK");
            return;
        }

        try
        {
            //create the user
            var user = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(email.Text, password.Text);
            // add the UID and the rest of the info here
            //user.User.Uid
            AuthService.UserCreds = user;
            user_.Uid = user.User.Uid;
            await App.UserRepo.UserDb.AddItemsAsync(user_);

            // Now that the user is authenticated, set the BindingContext for specific views
            var appShell = Shell.Current as AppShell;
            appShell.SetNavigationBindingContext();


            await DisplayAlert("Success", "Successfully sign up", "OK");
            await Shell.Current.GoToAsync($"//Index");
            await AuthService.Client.SignInWithEmailAndPasswordAsync(email.Text, password.Text);
            App.UserRepo.User = user_;





        }
        catch (FirebaseAuthException ex)
        {
            switch (ex.Reason)
            {
                case AuthErrorReason.EmailExists:
                    await DisplayAlert("Error", "A user with this email exists.", "OK");
                    break;
                case AuthErrorReason.AccountExistsWithDifferentCredential:
                    await DisplayAlert("Error", "Account already exists.", "OK");
                    break;
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
                case AuthErrorReason.UserDisabled:
                    await DisplayAlert("Error", "Account has been disabled.", "OK");
                    break;
                case AuthErrorReason.WeakPassword:
                    await DisplayAlert("Error", "Password is too weak. Must be at least 6 characters", "OK");
                    break;
                case AuthErrorReason.UserNotFound:
                    await DisplayAlert("Error", "Account not found.", "OK");
                    break;
                default:
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
            await DisplayAlert("Error", "Wrong username or password ", "OK");
        }
    }
}