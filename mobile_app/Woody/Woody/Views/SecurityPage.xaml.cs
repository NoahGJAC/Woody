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
			ChartsRepo.GetNoiseChart(App.SecurityRepo.NoiseLevels),
			ChartsRepo.GetLuminosityChart(App.SecurityRepo.LuminosityLevels)
		};

		SetBindingContext();
    }

    private void SetBindingContext()
    {
        BindingContext = App.SecurityRepo;

        frame_latest_readings.BindingContext = App.UserRepo.User.UserType;
        frame_graphs.BindingContext = App.UserRepo.User.UserType;
        frame_motion.BindingContext = App.UserRepo.User.UserType;
        grid_buzzer.BindingContext = App.UserRepo.User.UserType;

        ChartCarousel.BindingContext = this;
        IndicatorView.BindingContext = this;

        label_lum_current.BindingContext = App.SecurityRepo;
        label_noise_current.BindingContext = App.SecurityRepo;
        label_motion.BindingContext = App.SecurityRepo;
        BuzzerSwitch.BindingContext = App.SecurityRepo;
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