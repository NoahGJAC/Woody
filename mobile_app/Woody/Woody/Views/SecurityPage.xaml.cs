using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Woody.DataRepos;
using Woody.Enums;

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
    private Models.Command<string> BuzzerCommand { get; set; }

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
        BuzzerCommand = new Models.Command<string>("off",CommandType.BUZZER_ON_OFF,SubSystemType.Security);
		SetBindingContext();
    }

    private void SetBindingContext()
    {
        BindingContext = App.FarmRepo.SecurityRepo;

        frame_latest_readings.BindingContext = App.UserRepo.User.UserType;
        frame_graphs.BindingContext = App.UserRepo.User.UserType;
        frame_motion.BindingContext = App.UserRepo.User.UserType;
        grid_buzzer.BindingContext = App.UserRepo.User.UserType;

        ChartCarousel.BindingContext = this;
        IndicatorView.BindingContext = this;

        label_lum_current.BindingContext = App.FarmRepo.SecurityRepo;
        label_noise_current.BindingContext = App.FarmRepo.SecurityRepo;
        label_motion.BindingContext = App.FarmRepo.SecurityRepo;
        BuzzerSwitch.BindingContext = App.FarmRepo.SecurityRepo;
    }

    private async void BuzzerSwitch_Toggled(object sender, ToggledEventArgs e)
    {

        if (e.Value)
        {
            BuzzerCommand.Value = "on";
            await App.IoTDevice.SendCommandAsync(BuzzerCommand);
        }
        else
        {
            BuzzerCommand.Value = "off";
            await App.IoTDevice.SendCommandAsync(BuzzerCommand);
        }
    }

    private void ButtonLock_Clicked(object sender, EventArgs e)
    {
		App.FarmRepo.SecurityRepo.LockState.Value = !App.FarmRepo.SecurityRepo.LockState.Value;
    }
}