namespace Woody.Views;

public partial class AddTaskPage : ContentPage
{
    private Models.Tasks task_ { get;set; }
	public AddTaskPage()
	{
        InitializeComponent();
        task_ = new Models.Tasks();
        BindingContext = task_;
    }
}