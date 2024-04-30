
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
            await DisplayAlert("Error", "No internet", "OK");
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
            await DisplayAlert("Success", "Successfully sign up", "OK");
            await Shell.Current.GoToAsync($"//Index");
            await AuthService.Client.SignInWithEmailAndPasswordAsync(email.Text, password.Text);
            App.UserRepo.User = user_;





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
            await DisplayAlert("Error", "Wrong username or password ", "OK");
        }
    }
}