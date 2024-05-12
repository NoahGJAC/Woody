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

    }
	/// <summary>
	/// add the binding depending if the user is signIn or signUp
	/// </summary>
	private void CheckUserBinding()
	{
		if(App.UserRepo.User != null) {
            BindingContext = App.UserRepo.User;
        }
		else
		{
			var usr = AuthService.UserCreds;

			var temp = App.UserRepo.UserDb.Items.Where(u=>u.Uid==usr.User.Uid).First();
			App.UserRepo.User = temp;
            BindingContext = App.UserRepo.User;


        }
	}

    private void Btn_ChangePassword_Clicked(object sender, EventArgs e)
    {

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