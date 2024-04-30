using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Woody.DataRepos;

namespace Woody.Views;

/// <summary>
/// Represents a security page that displays charts and controls for security settings.
/// </summary>
public partial class SecurityPage : ContentPage
{
    /// <summary>
    /// Gets or sets the collection of Cartesian charts displayed on the page.
    /// </summary>
    public ObservableCollection<CartesianChart> Charts { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityPage"/> class.
    /// </summary>
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