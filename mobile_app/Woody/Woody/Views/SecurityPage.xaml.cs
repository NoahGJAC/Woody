using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
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
		// ChartNoise.BindingContext = ChartsRepo.GetNoiseChart(App.SecurityRepo.NoiseLevels);
		ChartCarousel.BindingContext = this;

    }
}