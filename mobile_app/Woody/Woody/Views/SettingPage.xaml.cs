using Woody.Services;

namespace Woody.Views;

public partial class SettingPage : ContentPage
{
	/// <summary>
	/// This class is the back of the setting page that display the user information and allow them to change theire info
	/// </summary>
	public SettingPage()
	{
		InitializeComponent();
		BindingContext = App.UserRepo.User;
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
}