using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Woody.DataRepos;

namespace Woody.Views;

public partial class SecurityPage : ContentPage
{
    public ObservableCollection<CartesianChart> Charts { get; set; }
	public SecurityPage()
	{
		InitializeComponent();
		Charts = new ObservableCollection<CartesianChart>
		{
			ChartsRepo.GetNoiseChart(App.SecurityRepo.NoiseLevels),
			ChartsRepo.GetLuminosityChart(App.SecurityRepo.LuminosityLevels)
		};

		BindingContext = App.SecurityRepo;
		ChartCarousel.BindingContext = this;
		IndicatorView.BindingContext = this;
    }

    private void BuzzerSwitch_Toggled(object sender, ToggledEventArgs e)
    {
		App.SecurityRepo.BuzzerState.Value = e.Value;
    }

    private void ButtonLock_Clicked(object sender, EventArgs e)
    {
		App.SecurityRepo.LockState.Value = !App.SecurityRepo.LockState.Value;
    }
}