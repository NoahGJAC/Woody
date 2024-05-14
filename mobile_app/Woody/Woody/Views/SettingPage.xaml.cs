using Firebase.Auth;
using Microsoft.Maui.ApplicationModel.Communication;
using Woody.Services;

namespace Woody.Views;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

/// <summary>
/// This class is the back of the setting page that display the user information and allow them to change theire info
/// </summary>
public partial class SettingPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingPage"/> class.
    /// </summary>
    public SettingPage()
	{
		InitializeComponent();
		CheckUserBinding();
        App.UserRepo.UserDb.LoadItemsAsync();

    }
    /// <summary>
    /// add the binding depending if the user is signIn or signUp
    /// </summary>
    private void CheckUserBinding()
    {
        if (App.UserRepo.User != null)
        {
            BindingContext = App.UserRepo.User;
        }
        else
        {
            var usr = AuthService.UserCreds;

            if (usr != null && usr.User != null && App.UserRepo.UserDb.Items != null && App.UserRepo.UserDb.Items.Any())
            {
                var temp = App.UserRepo.UserDb.Items.Where(u => u.Uid == usr.User.Uid).FirstOrDefault();
                if (temp != null)
                {
                    App.UserRepo.User = temp;
                    BindingContext = App.UserRepo.User;
                }
            }
        }
    }


    private async void Btn_ChangePassword_Clicked(object sender, EventArgs e)
    {
        try
        {
            await AuthService.Client.ResetEmailPasswordAsync(AuthService.Client.User.Info.Email);
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.Reason == AuthErrorReason.ResetPasswordExceedLimit)
            {
                await DisplayAlert("Error", "You have exceeded the limit for password resets. Please try again later.", "OK");
                return;
            }else
            {
                await DisplayAlert("Error", "An error occurred while trying to reset your password. Please try again later.", "OK");
                return;
            }
        }
        await DisplayAlert("Password Reset", $"An email has been sent to {AuthService.Client.User.Info.Email} to reset your password.", "OK");
    }

    private async void Btn_LogOut_Clicked(object sender, EventArgs e)
    {
        AuthService.Client.SignOut();
        await Shell.Current.GoToAsync($"//LoginPage");
    }

    private void Btn_ChangePFP_Clicked(object sender, EventArgs e)
    {

    }
}