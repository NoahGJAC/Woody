using Woody.Enums;

namespace Woody.Views;

public partial class AddTaskPage : ContentPage
{
    private Models.Tasks task_ { get;set; }

    public List<string> TagNames
    {
        get
        {
            return Enum.GetNames(typeof(TaskType))
                      .Select(name => name.ToLower())
                      .ToList();
        }
    }

    public AddTaskPage()
	{
        InitializeComponent();
        task_ = new Models.Tasks();
        BindingContext = task_;
        PickerTags.ItemsSource = TagNames;
    }

    private void DateTimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (task_ != null)
            task_.Date = new DateTime(
                datePicker.Date.Year,
                datePicker.Date.Month,
                datePicker.Date.Day,
                timePicker.Time.Hours,
                timePicker.Time.Minutes,
                timePicker.Time.Seconds
                );
    }

    private async void Add_Btn_Clicked(object sender, EventArgs e)
    {
        if (PickerTags.SelectedIndex == -1)
        {
            await DisplayAlert("Missing Data", "Please pick a meal?", "OK");
            return;
        }
        App.UserRepo.User.Tasks.Add(task_);
        await App.UserRepo.UserDb.UpdateItemsAsync(App.UserRepo.User);
        
        await Navigation.PopAsync();
    }

    private async void Cancel_btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}