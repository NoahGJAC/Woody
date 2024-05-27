using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using Woody.Enums;

namespace Woody.Views;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

/// <summary>
/// Represents a tasks page for the user.
/// </summary>
public partial class TasksPage : ContentPage, INotifyPropertyChanged
{

    /// <summary>
    /// Initializes a new instance of the <see cref="TasksPage"/> class.
    /// </summary>
    public TasksPage()
	{
        if(App.UserRepo.User.Tasks == null)
        {
            App.UserRepo.User.Tasks = new List<Models.Tasks>();
        }
        else
        {
            orderTasks();
        }
		InitializeComponent();
        BindingContext = App.UserRepo.User;
    }

    private async void AddTask_Btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
        orderTasks();
    }

    private void orderTasks()
    {
        App.UserRepo.User.Tasks = App.UserRepo.User.Tasks.OrderBy(t => t.Date).OrderBy(g => g.Tag).OrderBy(v => v.Title).ToList();
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
