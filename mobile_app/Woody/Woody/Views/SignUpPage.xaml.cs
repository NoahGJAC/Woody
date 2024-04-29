
using Firebase.Auth;
using Woody.Enums;
using Woody.Services;

namespace Woody.Views;

public partial class SignUpPage : ContentPage
{
    Models.User user_ { get; set; }
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
	}

    private async Task Btn_SignUp_ClickedAsync(object sender, EventArgs e)
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