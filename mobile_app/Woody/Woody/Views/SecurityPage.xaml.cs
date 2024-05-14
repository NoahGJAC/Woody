using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Woody.DataRepos;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

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
			ChartsRepo.GetNoiseChart(App.FarmRepo.SecurityRepo.NoiseLevels),
			ChartsRepo.GetLuminosityChart(App.FarmRepo.SecurityRepo.LuminosityLevels)
		};

		BindingContext = App.FarmRepo.SecurityRepo;
		ChartCarousel.BindingContext = this;
		IndicatorView.BindingContext = this;
    }

    private void BuzzerSwitch_Toggled(object sender, ToggledEventArgs e)
    {
		App.FarmRepo.SecurityRepo.BuzzerState.Value = e.Value;
    }

    private void ButtonLock_Clicked(object sender, EventArgs e)
    {
		App.FarmRepo.SecurityRepo.LockState.Value = !App.FarmRepo.SecurityRepo.LockState.Value;
    }
}